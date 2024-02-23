using AutoMapper;
using SistemaComercial.BLL.Servicios.Contrato;
using SistemaComercial.BLL.Servicios_Tareas.Contrato;
using SistemaComercial.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaComercial.BLL.Servicios_Tareas
{
    public class CotizacionLogicaService : ICotizacionLogica
    {
        private readonly IDetalleCotizacionVariableService _detalleCotizacionVariableService;
        private readonly ICotizacionService _cotizacionService;
        private readonly IVariablesEconomicasService _variableEconomicaService;
        private readonly IMapper _mapper;

        //Variables economicas 
        private decimal SMLV;
        private decimal tarifa;
        private decimal vpsv;
        private decimal vpd;
        private decimal vpn;
        private bool armado;
        private int aiuArmado;
        private int aiuSinArma;
        private int diasServicio;
        private int minutosInicioServicio;
        private int minutosFinServicio;
        private int minutosInicioDiurna;
        private int minutosFinDiurna;
        private int diasRequeridoServicio;
        private int horasJornadaDiurna;
        private int horasJornadaNocturna;
        private int cantidadServicios;

        public CotizacionLogicaService(IDetalleCotizacionVariableService detalleCotizacionVariableService, ICotizacionService cotizacionService, IVariablesEconomicasService variablesEconomicasService, IMapper mapper)
        {
            _detalleCotizacionVariableService = detalleCotizacionVariableService;
            _variableEconomicaService = variablesEconomicasService;
            _cotizacionService = cotizacionService;
            _mapper = mapper;
        }

        private decimal CalcularTarifa(decimal SMLV, decimal tarifa, decimal vpsv, bool armado, int aiuArmado, int aiuSinArma)
        {
            decimal calculoTarifa = 0;
            if (armado)
            {
                calculoTarifa = SMLV * tarifa + vpsv;
                decimal porcentaje = calculoTarifa * aiuArmado / 100;
                calculoTarifa += porcentaje;
            }
            else
            {
                calculoTarifa = SMLV * tarifa + vpsv;
                decimal porcentaje = calculoTarifa * aiuSinArma / 100;
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
                if (i == diasSemana.Length - 1)
                {
                    contador = contador + 2;
                }
            }
            return contador;
        }

        private decimal[] ContarHorasDiurnasNocturnas(int minutosInicio, int minutosFin, int minutosInicioDiurna, int minutosFinDiurna)
        {
            decimal minutosDiurnos = 0;
            decimal minutosNocturnos = 0;
            if (minutosInicio == minutosFin)
            {
                minutosDiurnos = 15 * 60;
                minutosNocturnos = 9 * 60;
            }
            else if (minutosInicio <= minutosFin)
            {
                if (minutosInicio < minutosInicioDiurna)
                {
                    minutosDiurnos += Math.Min(minutosFin, minutosFinDiurna) - minutosInicioDiurna;
                    minutosNocturnos += minutosInicioDiurna - minutosInicio;
                    if (minutosFin > minutosFinDiurna)
                    {
                        minutosNocturnos += minutosFin - minutosFinDiurna;
                    }
                }
                else if (minutosInicio < minutosFinDiurna && minutosInicio >= minutosInicioDiurna)
                {
                    minutosDiurnos *= Math.Min(minutosFin, minutosFinDiurna) - minutosInicio;
                    minutosNocturnos += minutosFin - minutosFinDiurna;
                }
                else
                {
                    minutosNocturnos += minutosFin - minutosInicio;
                }
            }
            else
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
            decimal[] listaHoras = { horasDiurnas, horasNocturnas };

            return listaHoras;
        }

        private decimal FormulaServicioVigilancia(decimal tarifa, decimal vprop, int dias, int jornada, decimal horasAtrabajar)
        {
            decimal tarifaMes = (tarifa * (vprop / 100)) / 30;
            decimal valorXdias = tarifaMes * dias;
            decimal valorXhorasLab = (valorXdias / jornada) * horasAtrabajar;
            return valorXhorasLab;
        }
        public decimal CalculoValorDellate(decimal SMLV, decimal tarifa, decimal vpsv, bool armado, int aiuArmado, int aiuSinArma, int desdeHora, int hastaHora, bool[] diasSemana, decimal horasDiurnas, decimal horasNocturnas, int minutosInicioDiurna, int minutosFinDiurna)
        {
            decimal tarifaServicio = CalcularTarifa(SMLV, tarifa, vpsv, armado, aiuArmado, aiuSinArma);
            decimal[] horas = ContarHorasDiurnasNocturnas(desdeHora, hastaHora, minutosInicioDiurna, minutosFinDiurna);
            int dias = SumarDias(diasSemana);
            throw new NotImplementedException();
        }

        public async Task<bool> RegistrarCotizacion(CotizacionDTO cotizacion)
        {
            try
            {


                //Necesitamos crear el detalle cotización con el valor para luego crear las variables economicas

                // Creee la cotizacion
                CotizacionDTO cotizacionCreada = await _cotizacionService.CrearCotizacion(cotizacion);

                //Seteamos las variables para obtener el valor de la cotización y crear el detalle
                foreach (var detalle in cotizacion.DatosDetalleCotizacion)
                {
                    cantidadServicios = detalle.CantidadServicios;
                    diasServicio = detalle.DiasServicio;
                    minutosInicioServicio = detalle.MinutosInicioServicio;
                    minutosFinServicio = detalle.MinutosFinServicio;
                    SMLV = detalle.Smlv;
                    armado = detalle.Armado;

                    // Obtengamos las variables faltantes
                    tarifa = await _variableEconomicaService.ListarVariable("Tarifa");
                    vpsv = await _variableEconomicaService.ListarVariable("Valor prima seguro de vida");
                    vpd = await _variableEconomicaService.ListarVariable("Valor proporcionalidad diurna");
                    vpn = await _variableEconomicaService.ListarVariable("Valor proporcionalidad nocturna");




                }

                // Recibimos la cotización, dentro de la cotización tenemos la información de los detalles
                // Procedemos a crear dentro de un bucle esas variables dentro de la tabla variables economicas
                foreach (DetalleCotizacionVariableDTO info in cotizacion.DatosDetalleCotizacion)
                {

                    
                    _detalleCotizacionVariableService.CrearDetalleVariable(info);
                }

            }
            catch
            {

            }
            throw new NotImplementedException();
        }
    }
}
