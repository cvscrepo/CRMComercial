using SistemaComercial.DAL.Repositorios.Contratos;
using SistemaComercial.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using SistemaComercial.Model;
using System.Runtime.InteropServices;
using SistemaComercial.DTO;
using System.Diagnostics;
using Microsoft.EntityFrameworkCore.Query;

namespace SistemaComercial.DAL.Repositorios
{
    public class CotizacionRepository : GenericRepository<Cotizacion>, ICotizacionRepository
    {
        private readonly DbcomercialContext _dbComercialContext;
        
        private string tarifa {  get; set; }
        public CotizacionRepository(DbcomercialContext dbComercialContext) : base(dbComercialContext) 
        {
            _dbComercialContext = dbComercialContext;
        }

        private decimal CalcularTarifa(decimal SMLV,decimal tarifa,decimal vpsv,bool armado,decimal aiuArmado,decimal aiuSinArma)
        {
            decimal calculoTarifa = 0;
            if (armado)
            {
                calculoTarifa = SMLV * tarifa + vpsv;
                var porcentaje = calculoTarifa * aiuArmado / 100;
                calculoTarifa += porcentaje;
            }
            else
            {
                calculoTarifa = SMLV * tarifa + vpsv;
                var porcentaje = calculoTarifa * aiuSinArma / 100;
                calculoTarifa += porcentaje;
            }
            return calculoTarifa;
        }

        private int SumarDias(bool[] diasSemana)
        {
            var contador = 0;
            for (int i = 0; i < diasSemana.Length; i++)
            {
                if (diasSemana[i] == true && i != diasSemana.Length - 1)
                {
                    contador = contador + 4;
                }
                if(i == diasSemana.Length - 1)
                {
                    contador = contador + 2;
                }
            }
            return contador;
        }

        private List<decimal> ContarHorasDiurnasNocturnas(int minutosInicio, int minutosFin, int minutosInicioDiurna, int minutosFinDiurna)
        {
            decimal minutosDiurnos = 0;
            decimal minutosNocturnos = 0;
            if(minutosInicio == minutosFin)
            {
                minutosDiurnos = 15 * 60;
                minutosNocturnos = 9 * 60;
            }
            else if (minutosInicio <= minutosFin)
            {
                if(minutosInicio < minutosInicioDiurna)
                {
                    minutosDiurnos += Math.Min(minutosFin, minutosFinDiurna) - minutosInicioDiurna;
                    minutosNocturnos += minutosInicioDiurna - minutosInicio;
                    if (minutosFin > minutosFinDiurna)
                    {
                        minutosNocturnos += minutosFin - minutosFinDiurna;
                    }
                }else if(minutosInicio < minutosFinDiurna && minutosInicio >= minutosInicioDiurna)
                {
                    minutosDiurnos *= Math.Min(minutosFin, minutosFinDiurna) - minutosInicio;
                    minutosNocturnos += minutosFin - minutosFinDiurna;
                }
                else
                {
                    minutosNocturnos += minutosFin - minutosInicio;
                }
            }else
            {

                if (minutosInicio <= minutosInicioDiurna)
                {
                    minutosNocturnos += minutosInicioDiurna - minutosInicio + 3 + minutosFin;
                    minutosDiurnos += minutosFinDiurna - minutosInicioDiurna;
                    //Buscar los diurnos antes de las 21:00
                }
                else if (minutosInicio >= minutosFinDiurna)
                {
                    minutosNocturnos += (24 * 60) - minutosInicio;
                    if (minutosFin > minutosInicioDiurna && minutosFin <= minutosFinDiurna)
                    {
                        minutosDiurnos += minutosFin - minutosInicioDiurna;
                        minutosNocturnos += 6 * 60;
                    }
                    else if (minutosFin > minutosInicioDiurna && minutosFin > minutosFinDiurna)
                    {
                        minutosNocturnos += minutosFin - minutosFinDiurna;
                    }
                    else
                    {
                        minutosNocturnos += minutosFin;
                    }
                }
                else
                {


                    if (minutosFin <= minutosInicioDiurna)
                    {
                        minutosNocturnos += minutosFin;

                    }
                    else
                    {
                        minutosDiurnos += minutosFin - minutosInicioDiurna;
                        minutosNocturnos += 6 * 60;

                    }

                    minutosDiurnos += minutosFinDiurna - minutosInicio;
                    minutosNocturnos += 3 * 60;

                }
            }

            decimal horasDiurnas = Math.Floor((decimal)minutosDiurnos / 60) + (minutosDiurnos % 60) / 60;
            decimal horasNocturnas = Math.Floor((decimal)minutosNocturnos / 60) + (minutosNocturnos % 60) / 60;

            List<decimal> listaHoras = new List<decimal>();
            listaHoras.Add(horasDiurnas);
            listaHoras.Add(horasNocturnas);
            return listaHoras;
        }

        private decimal FormulaServicioVigilancia(decimal tarifa,decimal vprop,int dias,int jornada,decimal horasAtrabajar)
        {
            decimal tarifaMes = (tarifa * (vprop / 100)) / 30;
            decimal valorXdias = tarifaMes * dias;
            decimal valorXhorasLab = (valorXdias / jornada) * horasAtrabajar;
            return valorXhorasLab;
        }


        private decimal CalculoValorDellate(decimal SMLV, decimal tarifa, decimal vpsv, bool armado, int aiuArmado, int aiuSinArma, int desdeHora, int hastaHora, bool[] diasSemana, decimal horasDiurnas, decimal horasNocturnas)
        {
            decimal tarifaServicio = CalcularTarifa(SMLV, tarifa, vpsv, armado, aiuArmado, aiuSinArma);
            decimal horas = ContarHorasDiurnasNocturnas(desdeHora, hastaHora);
        }

        public Task<Cotizacion> RegistrarDetalleCotización(CotizacionDTO cotizacion)
        {
            Cotizacion cotizacionGenerada = new Cotizacion();

            using (var transaction = _dbComercialContext.Database.BeginTransaction())
            {
                try
                {
                    foreach (DetalleCotizacion detalle in cotizacion.DetalleCotizacions)
                    {
                        //Aquí se genera la creación del detalleCotizacion junto con la lógica de calcular el total
                        var formula = detalle.IdServicioNavigation.TipoServicioNavigation.Formula;
                        if (detalle.IdServicioNavigation.TipoServicioNavigation.Nombre.ToLower() == "servicio vigilancia")
                        {
                          
                        }
                    }
                }
                catch
                {
                    throw;
                }
                return cotizacionGenerada;
            }
        }
    }
}
