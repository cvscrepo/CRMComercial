using Microsoft.Data.SqlClient;
using SistemaComercial.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaComercial.BLL.Servicios.Contrato
{
    public interface IRolService
    {
        public List<RolDTO> ListRoles();
        public List<RolDTO> ListRole(int id);
        public int CreateRol(RolDTO rol);
        public bool UpdateRole(RolDTO rol);
        public bool DeleteRole(int id = 0); 
    }
}
