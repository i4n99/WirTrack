﻿using System;
using System.Collections.Generic;

namespace Dominio
{
    public class Viajes
    {
        public int IdViaje { get; set; }
        public int IdCiudad { get; set; }
        public string IdsVehiculos { get; set; }
        public DateTime Fecha { get; set; }
        public string NombreReserva { get; set; }

        public Ciudades Ciudad { get; set; }
    }
}
