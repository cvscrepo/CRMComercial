using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using SistemaComercial.Model;

namespace SistemaComercial.DAL;

public partial class DbcomercialContext : DbContext
{
    public DbcomercialContext()
    {
    }

    public DbcomercialContext(DbContextOptions<DbcomercialContext> options)
        : base(options)
    {
    }

    public virtual DbSet<CategoriaInventario> CategoriaInventarios { get; set; }

    public virtual DbSet<Cliente> Clientes { get; set; }

    public virtual DbSet<Contrato> Contratos { get; set; }

    public virtual DbSet<Cotizacion> Cotizacions { get; set; }

    public virtual DbSet<DetalleCotizacion> DetalleCotizacions { get; set; }

    public virtual DbSet<DetalleCotizacionInventario> DetalleCotizacionInventarios { get; set; }

    public virtual DbSet<DetalleCotizacionVariable> DetalleCotizacionVariables { get; set; }

    public virtual DbSet<Inventario> Inventarios { get; set; }

    public virtual DbSet<Menu> Menus { get; set; }

    public virtual DbSet<MenuRol> MenuRols { get; set; }

    public virtual DbSet<NumeroDocumento> NumeroDocumentos { get; set; }

    public virtual DbSet<Rol> Rols { get; set; }

    public virtual DbSet<Servicio> Servicios { get; set; }

    public virtual DbSet<Sucursal> Sucursals { get; set; }

    public virtual DbSet<TipoServicio> TipoServicios { get; set; }

    public virtual DbSet<Usuario> Usuarios { get; set; }

    public virtual DbSet<VariablesEconomicas> VariablesEconomicas { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<CategoriaInventario>(entity =>
        {
            entity.HasKey(e => e.IdCategoria).HasName("PK__Categori__8A3D240CBB496165");

            entity.ToTable("CategoriaInventario");

            entity.Property(e => e.IdCategoria).HasColumnName("idCategoria");
            entity.Property(e => e.Nombre)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("nombre");
        });

        modelBuilder.Entity<Cliente>(entity =>
        {
            entity.HasKey(e => e.IdCliente).HasName("PK__Cliente__885457EEF0117591");

            entity.ToTable("Cliente");

            entity.Property(e => e.IdCliente).HasColumnName("idCliente");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("createdAt");
            entity.Property(e => e.CreatedBy).HasColumnName("createdBy");
            entity.Property(e => e.Email)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("email");
            entity.Property(e => e.Nit).HasColumnName("nit");
            entity.Property(e => e.NombreCompleto)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("nombreCompleto");
            entity.Property(e => e.NombreContacto)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("nombreContacto");
            entity.Property(e => e.Prospecto)
                .HasDefaultValueSql("((1))")
                .HasColumnName("prospecto");
            entity.Property(e => e.Telefono)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("telefono");
            entity.Property(e => e.UptadtedAt)
                .HasColumnType("datetime")
                .HasColumnName("uptadtedAt");
            entity.Property(e => e.UptadtedBy).HasColumnName("uptadtedBy");

            entity.HasOne(d => d.CreatedByNavigation).WithMany(p => p.ClienteCreatedByNavigations)
                .HasForeignKey(d => d.CreatedBy)
                .HasConstraintName("FK__Cliente__created__534D60F1");

            entity.HasOne(d => d.UptadtedByNavigation).WithMany(p => p.ClienteUptadtedByNavigations)
                .HasForeignKey(d => d.UptadtedBy)
                .HasConstraintName("FK__Cliente__uptadte__5441852A");
        });

        modelBuilder.Entity<Contrato>(entity =>
        {
            entity.HasKey(e => e.id).HasName("PK__Contrato__DF97D0E5F72E33EB");

            entity.ToTable("Contrato");

            entity.Property(e => e.id)
                .HasColumnName("id");
            entity.Property(e => e.FechaCreacion)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("fechaCreacion");
            entity.Property(e => e.FechaEdicion)
                .HasColumnType("datetime")
                .HasColumnName("fechaEdicion");
            entity.Property(e => e.Nit)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("nit");
            entity.Property(e => e.IdCotizacion).HasColumnName("idCotizacion");
            entity.Property(e => e.Nombre)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("nombre");
            entity.Property(e => e.RepresentanteLegal)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("representanteLegal");

            entity.HasOne(d => d.IdCotizacionNavigation).WithMany(p => p.Contratos)
                .HasForeignKey(d => d.IdCotizacion)
                .HasConstraintName("FK__Contrato__idCoti__6383C8BA");
        });

        modelBuilder.Entity<Cotizacion>(entity =>
        {
            entity.HasKey(e => e.IdCotizacion).HasName("PK__Cotizaci__D931C39B932BCE20");

            entity.ToTable("Cotizacion");

            entity.Property(e => e.IdCotizacion).HasColumnName("idCotizacion");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("createdAt");
            entity.Property(e => e.EditadoPor).HasColumnName("editadoPor");
            entity.Property(e => e.Estado)
                .HasDefaultValueSql("((2))")
                .HasColumnName("estado");
            entity.Property(e => e.IdCliente).HasColumnName("idCliente");
            entity.Property(e => e.IdUsuario).HasColumnName("idUsuario");
            entity.Property(e => e.Nombre)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("nombre");
            entity.Property(e => e.Total)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("total");
            entity.Property(e => e.UpdatedAt)
                .HasColumnType("datetime")
                .HasColumnName("updatedAt");

            entity.HasOne(d => d.EditadoPorNavigation).WithMany(p => p.CotizacionEditadoPorNavigations)
                .HasForeignKey(d => d.EditadoPor)
                .HasConstraintName("FK__Cotizacio__edita__5EBF139D");

            entity.HasOne(d => d.IdClienteNavigation).WithMany(p => p.Cotizacions)
                .HasForeignKey(d => d.IdCliente)
                .HasConstraintName("FK__Cotizacio__idCli__5CD6CB2B");

            entity.HasOne(d => d.IdUsuarioNavigation).WithMany(p => p.CotizacionIdUsuarioNavigations)
                .HasForeignKey(d => d.IdUsuario)
                .HasConstraintName("FK__Cotizacio__idUsu__5DCAEF64");
        });

        modelBuilder.Entity<DetalleCotizacion>(entity =>
        {
            entity.HasKey(e => e.IdDetalleCotizacion).HasName("PK__DetalleC__3152BA3D89359B95");

            entity.ToTable("DetalleCotizacion");

            entity.Property(e => e.IdDetalleCotizacion).HasColumnName("idDetalleCotizacion");
            entity.Property(e => e.CantidadServicios).HasColumnName("cantidadServicios");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("createdAt");
            entity.Property(e => e.DetalleServicio)
                .HasColumnType("text")
                .HasColumnName("detalleServicio");
            entity.Property(e => e.IdCotizacion).HasColumnName("idCotizacion");
            entity.Property(e => e.IdProducto).HasColumnName("idProducto");
            entity.Property(e => e.IdSucursal).HasColumnName("idSucursal");
            entity.Property(e => e.Total)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("total");
            entity.Property(e => e.UpdatedAt)
                .HasColumnType("datetime")
                .HasColumnName("updatedAt");

            entity.HasOne(d => d.IdCotizacionNavigation).WithMany(p => p.DetalleCotizacions)
                .HasForeignKey(d => d.IdCotizacion)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__DetalleCo__idCot__71D1E811");

            entity.HasOne(d => d.IdProductoNavigation).WithMany(p => p.DetalleCotizacions)
                .HasForeignKey(d => d.IdProducto)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__DetalleCo__idPro__72C60C4A");

            entity.HasOne(d => d.IdSucursalNavigation).WithMany(p => p.DetalleCotizacions)
                .HasForeignKey(d => d.IdSucursal)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__DetalleCo__idSuc__73BA3083");
        });

        modelBuilder.Entity<DetalleCotizacionInventario>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__DetalleC__3213E83FB06EC104");

            entity.ToTable("DetalleCotizacionInventario");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.CantidadAsignada).HasColumnName("cantidadAsignada");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("createdAt");
            entity.Property(e => e.CreatedBy).HasColumnName("createdBy");
            entity.Property(e => e.IdDetalleCotizacion).HasColumnName("idDetalleCotizacion");
            entity.Property(e => e.IdInventario).HasColumnName("idInventario");
            entity.Property(e => e.UpdatedAt)
                .HasColumnType("datetime")
                .HasColumnName("updatedAt");

            entity.HasOne(d => d.CreatedByNavigation).WithMany(p => p.DetalleCotizacionInventarios)
                .HasForeignKey(d => d.CreatedBy)
                .HasConstraintName("FK__DetalleCo__creat__787EE5A0");

            entity.HasOne(d => d.IdDetalleCotizacionNavigation).WithMany(p => p.DetalleCotizacionInventarios)
                .HasForeignKey(d => d.IdDetalleCotizacion)
                .HasConstraintName("FK__DetalleCo__idDet__778AC167");

            entity.HasOne(d => d.IdInventarioNavigation).WithMany(p => p.DetalleCotizacionInventarios)
                .HasForeignKey(d => d.IdInventario)
                .HasConstraintName("FK__DetalleCo__idInv__797309D9");
        });

        modelBuilder.Entity<DetalleCotizacionVariable>(entity =>
        {
            entity.HasKey(e => e.IdDetalleCotizacionVariables).HasName("PK__DetalleC__F4AD00FD79154BF9");

            entity.Property(e => e.IdDetalleCotizacionVariables).HasColumnName("idDetalleCotizacionVariables");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("createdAt");
            entity.Property(e => e.IdDetalleCotizacion).HasColumnName("idDetalleCotizacion");
            entity.Property(e => e.IdParametrosVariables).HasColumnName("idParametrosVariables");
            entity.Property(e => e.UpdatedAt)
                .HasColumnType("datetime")
                .HasColumnName("updatedAt");
            entity.Property(e => e.Valor)
                .HasColumnType("decimal(10, 0)")
                .HasColumnName("valor");

            entity.HasOne(d => d.IdDetalleCotizacionNavigation).WithMany(p => p.DetalleCotizacionVariables)
                .HasForeignKey(d => d.IdDetalleCotizacion)
                .HasConstraintName("FK__DetalleCo__idDet__7D439ABD");

            entity.HasOne(d => d.IdParametrosVariablesNavigation).WithMany(p => p.DetalleCotizacionVariables)
                .HasForeignKey(d => d.IdParametrosVariables)
                .HasConstraintName("FK__DetalleCo__idPar__7E37BEF6");
        });

        modelBuilder.Entity<Inventario>(entity =>
        {
            entity.HasKey(e => e.IdInventario).HasName("PK__Inventar__8F145B0D059CF7CC");

            entity.ToTable("Inventario");

            entity.Property(e => e.IdInventario).HasColumnName("idInventario");
            entity.Property(e => e.CantidadAsignada).HasColumnName("cantidadAsignada");
            entity.Property(e => e.CantidadDisponible).HasColumnName("cantidadDisponible");
            entity.Property(e => e.CantidadTotal).HasColumnName("cantidadTotal");
            entity.Property(e => e.CreateAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("createAt");
            entity.Property(e => e.CreatedBy).HasColumnName("createdBy");
            entity.Property(e => e.Estado)
                .HasDefaultValueSql("((0))")
                .HasColumnName("estado");
            entity.Property(e => e.IdCategoriaInventario).HasColumnName("idCategoriaInventario");
            entity.Property(e => e.Nombre)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("nombre");
            entity.Property(e => e.UptadedAt)
                .HasColumnType("datetime")
                .HasColumnName("uptadedAt");
            entity.Property(e => e.Valor)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("valor");

            entity.HasOne(d => d.CreatedByNavigation).WithMany(p => p.Inventarios)
                .HasForeignKey(d => d.CreatedBy)
                .HasConstraintName("FK__Inventari__creat__6A30C649");

            entity.HasOne(d => d.IdCategoriaInventarioNavigation).WithMany(p => p.Inventarios)
                .HasForeignKey(d => d.IdCategoriaInventario)
                .HasConstraintName("FK__Inventari__idCat__693CA210");
        });

        modelBuilder.Entity<Menu>(entity =>
        {
            entity.HasKey(e => e.IdMenu).HasName("PK__Menu__C26AF483044E6651");

            entity.ToTable("Menu");

            entity.Property(e => e.IdMenu).HasColumnName("idMenu");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("createdAt");
            entity.Property(e => e.Icono)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("icono");
            entity.Property(e => e.Nombre)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("nombre");
            entity.Property(e => e.UptadtedAt)
                .HasColumnType("datetime")
                .HasColumnName("uptadtedAt");
            entity.Property(e => e.Url)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("url");
        });

        modelBuilder.Entity<MenuRol>(entity =>
        {
            entity.HasKey(e => e.IdMenuRol).HasName("PK__MenuRol__9D6D61A43F736400");

            entity.ToTable("MenuRol");

            entity.Property(e => e.IdMenuRol).HasColumnName("idMenuRol");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("createdAt");
            entity.Property(e => e.IdMenu).HasColumnName("idMenu");
            entity.Property(e => e.IdRol).HasColumnName("idRol");
            entity.Property(e => e.UptadeAt)
                .HasColumnType("datetime")
                .HasColumnName("uptadeAt");

            entity.HasOne(d => d.IdMenuNavigation).WithMany(p => p.MenuRols)
                .HasForeignKey(d => d.IdMenu)
                .HasConstraintName("FK__MenuRol__idMenu__3D5E1FD2");

            entity.HasOne(d => d.IdRolNavigation).WithMany(p => p.MenuRols)
                .HasForeignKey(d => d.IdRol)
                .HasConstraintName("FK__MenuRol__idRol__3E52440B");
        });

        modelBuilder.Entity<NumeroDocumento>(entity =>
        {
            entity.HasKey(e => e.IdNumeroDocumento).HasName("PK__NumeroDo__471E421A418D9F83");

            entity.ToTable("NumeroDocumento");

            entity.Property(e => e.IdNumeroDocumento).HasColumnName("idNumeroDocumento");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("createdAt");
            entity.Property(e => e.UltimoNumero).HasColumnName("ultimoNumero");
        });

        modelBuilder.Entity<Rol>(entity =>
        {
            entity.HasKey(e => e.IdRol).HasName("PK__Rol__3C872F767C9F4EC1");

            entity.ToTable("Rol");

            entity.Property(e => e.IdRol).HasColumnName("idRol");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("createdAt");
            entity.Property(e => e.Nombre)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("nombre");
            entity.Property(e => e.UpdatedAt)
                .HasColumnType("datetime")
                .HasColumnName("updatedAt");
        });

        modelBuilder.Entity<Servicio>(entity =>
        {
            entity.HasKey(e => e.IdServicio).HasName("PK__Servicio__CEB9811910DE61D1");

            entity.ToTable("Servicio");

            entity.Property(e => e.IdServicio).HasColumnName("idServicio");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("createdAt");
            entity.Property(e => e.Descripcion)
                .HasColumnType("text")
                .HasColumnName("descripcion");
            entity.Property(e => e.Nombre)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("nombre");
            entity.Property(e => e.TipoServicio).HasColumnName("tipoServicio");
            entity.Property(e => e.UpdatedAt)
                .HasColumnType("datetime")
                .HasColumnName("updatedAt");

            entity.HasOne(d => d.TipoServicioNavigation).WithMany(p => p.Servicios)
                .HasForeignKey(d => d.TipoServicio)
                .HasConstraintName("FK__Servicio__tipoPr__4AB81AF0");
        });

        modelBuilder.Entity<Sucursal>(entity =>
        {
            entity.HasKey(e => e.IdSucursal).HasName("PK__Sucursal__F707694CFBEE7532");

            entity.ToTable("Sucursal");

            entity.Property(e => e.IdSucursal).HasColumnName("idSucursal");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("createdAt");
            entity.Property(e => e.Direccion)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("direccion");
            entity.Property(e => e.IdCliente).HasColumnName("idCliente");
            entity.Property(e => e.Nombre)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("nombre");
            entity.Property(e => e.UpdatedAt)
                .HasColumnType("datetime")
                .HasColumnName("updatedAt");

            entity.HasOne(d => d.IdClienteNavigation).WithMany(p => p.Sucursals)
                .HasForeignKey(d => d.IdCliente)
                .HasConstraintName("FK__Sucursal__idClie__59063A47");
        });

        modelBuilder.Entity<TipoServicio>(entity =>
        {
            entity.HasKey(e => e.IdTipoServicio).HasName("PK__TipoServ__27861676709774D0");

            entity.ToTable("TipoServicio");

            entity.Property(e => e.IdTipoServicio).HasColumnName("idTipoServicio");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("createdAt");
            entity.Property(e => e.Descripcion)
                .HasColumnType("text")
                .HasColumnName("descripcion");
            entity.Property(e => e.Formula)
                .HasMaxLength(250)
                .IsUnicode(false)
                .HasColumnName("formula");
            entity.Property(e => e.Nombre)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("nombre");
            entity.Property(e => e.UpdateAt)
                .HasColumnType("datetime")
                .HasColumnName("updateAt");
        });

        modelBuilder.Entity<Usuario>(entity =>
        {
            entity.HasKey(e => e.IdUsuario).HasName("PK__Usuario__645723A6AED46FD0");

            entity.ToTable("Usuario");

            entity.Property(e => e.IdUsuario).HasColumnName("idUsuario");
            entity.Property(e => e.Contrasena)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("contrasena");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("createdAt");
            entity.Property(e => e.Email)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("email");
            entity.Property(e => e.EsActivo)
                .HasDefaultValueSql("((1))")
                .HasColumnName("esActivo");
            entity.Property(e => e.IdRol).HasColumnName("idRol");
            entity.Property(e => e.LastConnection)
                .HasColumnType("datetime")
                .HasColumnName("lastConnection");
            entity.Property(e => e.NombreCompleto)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("nombreCompleto");
            entity.Property(e => e.UptadedAt)
                .HasColumnType("datetime")
                .HasColumnName("uptadedAt");
            entity.Property(e => e.UrlFoto)
                .HasMaxLength(200)
                .IsUnicode(false)
                .HasColumnName("urlFoto");
            entity.Property(e => e.UsuarioRegistro).HasColumnName("usuarioRegistro");

            entity.HasOne(d => d.IdRolNavigation).WithMany(p => p.Usuarios)
                .HasForeignKey(d => d.IdRol)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Usuario__idRol__4222D4EF");

            entity.HasOne(d => d.UsuarioRegistroNavigation).WithMany(p => p.InverseUsuarioRegistroNavigation)
                .HasForeignKey(d => d.UsuarioRegistro)
                .HasConstraintName("FK__Usuario__usuario__4316F928");
        });

        modelBuilder.Entity<VariablesEconomicas>(entity =>
        {
            entity.HasKey(e => e.IdVariablesEconomicas).HasName("PK__Variable__A9412766A6A5C029");

            entity.Property(e => e.IdVariablesEconomicas).HasColumnName("idVariablesEconomicas");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("createdAt");
            entity.Property(e => e.CreatedBy).HasColumnName("createdBy");
            entity.Property(e => e.Descripcion)
                .HasColumnType("text")
                .HasColumnName("descripcion");
            entity.Property(e => e.Nombre)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("nombre");
            entity.Property(e => e.UpdatedBy).HasColumnName("updatedBy");
            entity.Property(e => e.UpdatedAt)
                .HasColumnType("datetime")
                .HasColumnName("updatedAt");
            entity.Property(e => e.Valor)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("valor");

            entity.HasOne(d => d.CreatedByNavigation).WithMany(p => p.VariablesEconomicaCreatedByNavigations)
                .HasForeignKey(d => d.CreatedBy)
                .HasConstraintName("FK__Variables__creat__4E88ABD4");

            entity.HasOne(d => d.UpdatedByNavigation).WithMany(p => p.VariablesEconomicaUpdatedByNavigations)
                .HasForeignKey(d => d.UpdatedBy)
                .HasConstraintName("FK__Variables__updat__4F7CD00D");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
