using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Datos.Modelos;
using Negocio.Modelos;

namespace Negocio.Servicios
{
    public class FacturaServicio
    {
        private readonly ProductoServicio ps = new ProductoServicio();
        private readonly UsuarioServicio ususer = new UsuarioServicio();
        private readonly FacturaMetodos FacturaMetodos = new FacturaMetodos();

        /// <summary>
        /// Carga todas las facturas almacenadas en el sistema.
        /// </summary>
        /// <returns>Lista de facturas en formato <see cref="FacturaNegocio"/>.</returns>
        public async Task<List<FacturaNegocio>> CargarFacturas()
        {
            List<FacturaDatos> facturasDatos = await FacturaMetodos.CargarFacturas();
            List<FacturaNegocio> facturasNegocio = new List<FacturaNegocio>();

            foreach (FacturaDatos item in facturasDatos)
            {
                FacturaNegocio f = ConvertirAFacturaNegocio(item);
                facturasNegocio.Add(f);
            }

            return facturasNegocio;
        }


        /// <summary>
        /// Guarda una nueva factura en la base de datos.
        /// </summary>
        /// <param name="nuevaFactura">Instancia de <see cref="FacturaNegocio"/> que representa la nueva factura.</param>
        /// <returns>True si se guardó correctamente; false en caso contrario.</returns>
        public bool GuardarFactura(FacturaNegocio nuevaFactura)
        {
            var facturaDatos = ConvertirAFacturaDatos(nuevaFactura);
            return FacturaMetodos.GuardarFactura(facturaDatos);
        }

        /// <summary>
        /// Elimina una factura existente por su identificador.
        /// </summary>
        /// <param name="idFactura">Identificador de la factura a eliminar.</param>
        /// <returns>True si se eliminó correctamente; false en caso contrario.</returns>
        public bool EliminarFactura(int idFactura)
        {
            return FacturaMetodos.BorrarFacturaPorId(idFactura);
        }

        /// <summary>
        /// Marca una factura como guardada en el sistema.
        /// </summary>
        /// <param name="id">Identificador de la factura.</param>
        public void MarcarFacturaComoGuardada(int id)
        {
            FacturaMetodos.MarcarFacturaComoGuardada(id);
        }

        /// <summary>
        /// Convierte una instancia de <see cref="FacturaDatos"/> en una instancia de <see cref="FacturaNegocio"/>.
        /// </summary>
        /// <param name="facturaDatos">Factura en formato de datos.</param>
        /// <returns>Factura convertida al modelo de negocio.</returns>
        private FacturaNegocio ConvertirAFacturaNegocio(FacturaDatos facturaDatos)
        {
            FacturaNegocio f = new FacturaNegocio
            {
                Id = facturaDatos.Id,

                Vendedor = facturaDatos.Vendedor != null
                    ? new UsuarioNegocio
                    {
                        Id = facturaDatos.Vendedor.Id,
                        Nombre = facturaDatos.Vendedor.Nombre,
                        Rol = facturaDatos.Vendedor.Rol,
                        Correo = facturaDatos.Vendedor.Correo,
                        Localizacion = facturaDatos.Vendedor.Localizacion,
                        Foto = ususer.parseByteImage(facturaDatos.Vendedor.Foto)
                    }
                    : new UsuarioNegocio
                    {
                        Id = 0, 
                        Nombre = "Usuario eliminado",
                        Rol = "Desconocido",
                        Correo = "eliminado@dominio.com",
                        Localizacion = "Desconocida",
                        Foto = null
                    },

                Total = facturaDatos.Total,
                FechaVenta = facturaDatos.FechaVenta,
                ProductosVendidos = facturaDatos.ProductosVendidos
                    .Select(item => ps.toNegocio(item))
                    .ToList(),
                tarjeta = facturaDatos.Tarjeta,
                Guardada = facturaDatos.Guardada
            };

            return f;
        }

        /// <summary>
        /// Convierte una instancia de <see cref="FacturaNegocio"/> en una instancia de <see cref="FacturaDatos"/>.
        /// </summary>
        /// <param name="facturaNegocio">Factura en formato de negocio.</param>
        /// <returns>Factura convertida al modelo de datos.</returns>
        private FacturaDatos ConvertirAFacturaDatos(FacturaNegocio facturaNegocio)
        {
            FacturaDatos f = new FacturaDatos
            {
                Id = facturaNegocio.Id,
                Vendedor = new UsuarioDatos
                {
                    Id = facturaNegocio.Vendedor.Id,
                    Nombre = facturaNegocio.Vendedor.Nombre,
                    Rol = facturaNegocio.Vendedor.Rol,
                    Correo = facturaNegocio.Vendedor.Correo,
                    Localizacion = facturaNegocio.Vendedor.Localizacion,
                    Foto = ususer.parseImageBytea(facturaNegocio.Vendedor.Foto)
                },
                Total = facturaNegocio.Total,
                FechaVenta = DateTime.Now,
                ProductosVendidos = facturaNegocio.ProductosVendidos
                    .Select(item => ps.toDatos(item))
                    .ToList(),
                Tarjeta = facturaNegocio.tarjeta,
                Guardada = facturaNegocio.Guardada
            };

            return f;
        }
    }
}
