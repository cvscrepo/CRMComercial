using Microsoft.Data.SqlClient;
using SistemaComercial.DTO;
using SistemaComercial.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaComercial.BLL.Servicios.Contrato
{
    public interface IRolService
    {
        public Task<List<RolDTO>> ListRoles();
        public Task<List<RolDTO>> ListRole(int id);
        public Task<int> CreateRol(RolDTO rol);
        public Task<bool> UpdateRole(RolDTO rol);
        public Task<bool> DeleteRole(int id = 0); 
    }
}
