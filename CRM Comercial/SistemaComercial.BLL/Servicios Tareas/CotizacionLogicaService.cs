using AutoMapper;
using SistemaComercial.BLL.Servicios.Contrato;
using SistemaComercial.BLL.Servicios_Tareas.Contrato;
using SistemaComercial.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
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

        //Variables economicas detalle servicio
        private decimal SMLV;
        private bool armado;
        private int minutosInicioServicio;
        private int minutosFinServicio;
        private int diasRequeridoServicio;


        //Variables economicas 
        private decimal tarifa;
        private decimal vpsv;
        private decimal vpd;
        private decimal vpn;
        private int aiuArmado;
        private int aiuSinArma;
        private int minutosInicioDiurna;
        private int minutosFinDiurna;
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
                    minutosDiurnos += Math.Min(minutosFin, minutosFinDiurna) - minutosInicio;
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
        public decimal CalculoValorDellate(decimal SMLV, decimal tarifa, decimal vpd, decimal vpn, decimal vpsv, bool armado, int aiuArmado, int aiuSinArma, int desdeHora, int hastaHora, int diasServicio, int horasJornadaDiurna, int horasJornadaNocturna, int minutosInicioDiurna, int minutosFinDiurna)
        {
            decimal tarifaServicio = CalcularTarifa(SMLV, tarifa, vpsv, armado, aiuArmado, aiuSinArma);
            decimal[] horas = ContarHorasDiurnasNocturnas(desdeHora, hastaHora, minutosInicioDiurna, minutosFinDiurna);
            decimal valorHoraDia = 0;
            decimal valorHoraNoche = 0;

            if (horas[0] > 0)
            {
                valorHoraDia = FormulaServicioVigilancia(tarifaServicio, vpd, diasServicio, horasJornadaDiurna, horas[0]);
            }
            if (horas[1] > 0)
            {
                valorHoraNoche = FormulaServicioVigilancia(tarifaServicio, vpn, diasServicio, horasJornadaNocturna, horas[1]);
            }
            decimal totalValor = valorHoraDia + valorHoraNoche;
            decimal totalRedondeado = Math.Round(totalValor);
            return totalRedondeado;
        }

        public async Task<decimal> CalculoDetalleCotizacion(CotizacionDTO cotizacion)
        {
            try
            {
                decimal resultados = 0;

                // Obtengamos las variables de la base de datos
                tarifa = await _variableEconomicaService.ListarVariable("tarifa");
                vpsv = await _variableEconomicaService.ListarVariable("vpsv");
                vpd = await _variableEconomicaService.ListarVariable("vpd");
                vpn = await _variableEconomicaService.ListarVariable("vpn");
                aiuArmado = (int)await _variableEconomicaService.ListarVariable("aiuArmado");
                aiuSinArma = (int)await _variableEconomicaService.ListarVariable("aiuSinArma");
                minutosInicioDiurna = (int)await _variableEconomicaService.ListarVariable("minutosInicioDiurna");
                minutosFinDiurna = (int)await _variableEconomicaService.ListarVariable("minutosFinDiurna");
                horasJornadaDiurna = (int)await _variableEconomicaService.ListarVariable("horasJornadaDiurna");
                horasJornadaNocturna = (int)await _variableEconomicaService.ListarVariable("horasJornadaNocturna");

                //Seteamos las variables para obtener el valor de la cotización y crear el detalle

                foreach (DetalleCotizacionDTO detalle in cotizacion.DetalleCotizacions)
                {
                    foreach(DetalleCotizacionVariableDTO variable in detalle.DetalleCotizacionVariables)
                    {

                        var nombreVariable = variable.IdVariablesEconomicasNavigation.Nombre;
                        //decimal valorVariable = Convert.ToDecimal(variable.IdVariablesEconomicasNavigation.Valor);
                        decimal valorDetalleCotizacion = Convert.ToDecimal(variable.Valor);
                        Type propiedad = typeof(CotizacionLogicaService);
                        FieldInfo propiedad1 = propiedad.GetField(nombreVariable, BindingFlags.NonPublic | BindingFlags.Instance);

                        //Varificar si la propiedad existe

                        if (propiedad1 != null && propiedad1.FieldType == typeof(decimal))
                        {
                            propiedad1.SetValue(this, valorDetalleCotizacion);
                        }
                        if (propiedad1 != null && propiedad1.FieldType == typeof(bool))
                        {
                            propiedad1.SetValue(this, valorDetalleCotizacion == 1);
                        }
                        if (propiedad1 != null && propiedad1.FieldType == typeof(int))
                        {
                            propiedad1.SetValue(this, (int) valorDetalleCotizacion);
                        }

                    }
                    
                    // Hacer el calculo del valor del servicio 
                    decimal valorDetalleCotizacionCalculado = this.CalculoValorDellate( this.SMLV, this.tarifa, this.vpd, this.vpn, this.vpsv, this.armado, this.aiuArmado, this.aiuSinArma, this.minutosInicioServicio, this.minutosFinServicio , this.diasRequeridoServicio ,  this.horasJornadaDiurna,  this.horasJornadaNocturna,  this.minutosInicioDiurna,  this.minutosFinDiurna);
                    resultados = valorDetalleCotizacionCalculado;
                    
                    
                }

                return resultados;
            }
            catch
            {
                throw;
            }
            
        }
        
        // Se crea la cotización
        // Se saca el valor del detalle
        // Se crea el detalle cotizacion
        // Se crea las variables detalle cotización 
        // 

    }
}
