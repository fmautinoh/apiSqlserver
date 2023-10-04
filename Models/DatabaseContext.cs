using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace apiSqlserver.Models;

public partial class DatabaseContext : DbContext
{
    public DatabaseContext()
    {
    }

    public DatabaseContext(DbContextOptions<DatabaseContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Autenticidad> Autenticidads { get; set; }

    public virtual DbSet<Autore> Autores { get; set; }

    public virtual DbSet<EstadoConservacion> EstadoConservacions { get; set; }

    public virtual DbSet<InventarioLibro> InventarioLibros { get; set; }

    public virtual DbSet<Libro> Libros { get; set; }

    public virtual DbSet<LibrosAutore> LibrosAutores { get; set; }

    public virtual DbSet<TipoAutor> TipoAutors { get; set; }

    public virtual DbSet<TipoLibro> TipoLibros { get; set; }

    public virtual DbSet<TipoUsuario> TipoUsuarios { get; set; }

    public virtual DbSet<Usuario> Usuarios { get; set; }

    public virtual DbSet<VAutor> VAutors { get; set; }

    public virtual DbSet<VInvReporte> VInvReportes { get; set; }

    public virtual DbSet<VInventario> VInventarios { get; set; }

    public virtual DbSet<VLibro> VLibros { get; set; }

//    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
//#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
//        => optionsBuilder.UseSqlServer("Server=localhost, 14333;Database=inventariobiblioteca;Persist Security Info=False;User ID=sa; Password=Ro@t20042002;MultipleActiveResultSets=False;Encrypt=False;TrustServerCertificate=False;Connection Timeout=30;");

    protected override void OnModelCreating(ModelBuilder ModelBuilder)
    {
        ModelBuilder.Entity<Autenticidad>(entity =>
        {
            entity.HasKey(e => e.AutenticidadId).HasName("PK__autentic__3A49172C87595F76");

            entity.ToTable("autenticidad");

            entity.Property(e => e.AutenticidadId).HasColumnName("autenticidadID");
            entity.Property(e => e.autenticidad)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("autenticidad");
        });

        ModelBuilder.Entity<Autore>(entity =>
        {
            entity.HasKey(e => e.AutorId).HasName("PK__Autores__F58AE90910D8AD6A");

            entity.Property(e => e.AutorId).HasColumnName("AutorID");
            entity.Property(e => e.NombreAutor)
                .HasMaxLength(80)
                .IsUnicode(false);
            entity.Property(e => e.TipoAutorId).HasColumnName("TipoAutorID");

            entity.HasOne(d => d.TipoAutor).WithMany(p => p.Autores)
                .HasForeignKey(d => d.TipoAutorId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Autores__TipoAut__3D5E1FD2");
        });

        ModelBuilder.Entity<EstadoConservacion>(entity =>
        {
            entity.HasKey(e => e.EstadoId).HasName("PK__EstadoCo__FEF86B602F0006EC");

            entity.ToTable("EstadoConservacion");

            entity.Property(e => e.EstadoId).HasColumnName("EstadoID");
            entity.Property(e => e.Color)
                .HasMaxLength(7)
                .IsUnicode(false)
                .HasColumnName("color");
            entity.Property(e => e.Descripcion)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.Valor).HasColumnName("valor");
        });

        ModelBuilder.Entity<InventarioLibro>(entity =>
        {
            entity.HasKey(e => e.InventarioId).HasName("PK__Inventar__FB8A24B783D23630");

            entity.ToTable("InventarioLibro");

            entity.Property(e => e.InventarioId).HasColumnName("InventarioID");
            entity.Property(e => e.AutenticidadId).HasColumnName("autenticidadID");
            entity.Property(e => e.Codigo)
                .HasMaxLength(40)
                .IsUnicode(false);
            entity.Property(e => e.EstadoId).HasColumnName("EstadoID");
            entity.Property(e => e.LibroId).HasColumnName("LibroID");

            entity.HasOne(d => d.Autenticidad).WithMany(p => p.InventarioLibros)
                .HasForeignKey(d => d.AutenticidadId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Inventari__auten__48CFD27E");

            entity.HasOne(d => d.Estado).WithMany(p => p.InventarioLibros)
                .HasForeignKey(d => d.EstadoId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Inventari__Estad__49C3F6B7");

            entity.HasOne(d => d.Libro).WithMany(p => p.InventarioLibros)
                .HasForeignKey(d => d.LibroId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Inventari__Libro__4AB81AF0");
        });

        ModelBuilder.Entity<Libro>(entity =>
        {
            entity.HasKey(e => e.LibroId).HasName("PK__Libros__35A1EC8D56C63F05");

            entity.Property(e => e.LibroId).HasColumnName("LibroID");
            entity.Property(e => e.Editorial)
                .HasMaxLength(80)
                .IsUnicode(false);
            entity.Property(e => e.NombreLib)
                .HasMaxLength(80)
                .IsUnicode(false);
            entity.Property(e => e.TipoId).HasColumnName("TipoID");

            entity.HasOne(d => d.Tipo).WithMany(p => p.Libros)
                .HasForeignKey(d => d.TipoId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Libros__TipoID__4222D4EF");
        });

        ModelBuilder.Entity<LibrosAutore>(entity =>
        {
            entity.HasKey(e => e.LibroAutorId).HasName("LibrosAutores_pk");

            entity.Property(e => e.LibroAutorId).HasColumnName("LibroAutorID");
            entity.Property(e => e.AutorId).HasColumnName("AutorID");
            entity.Property(e => e.LibroId).HasColumnName("LibroID");

            entity.HasOne(d => d.Autor).WithMany(p => p.LibrosAutores)
                .HasForeignKey(d => d.AutorId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__LibrosAut__Autor__4E88ABD4");

            entity.HasOne(d => d.Libro).WithMany(p => p.LibrosAutores)
                .HasForeignKey(d => d.LibroId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__LibrosAut__Libro__4D94879B");
        });

        ModelBuilder.Entity<TipoAutor>(entity =>
        {
            entity.HasKey(e => e.TipoAutorId).HasName("PK__TipoAuto__39C500C34AFEE1AC");

            entity.ToTable("TipoAutor");

            entity.Property(e => e.TipoAutorId).HasColumnName("TipoAutorID");
            entity.Property(e => e.tipoautor)
                .HasMaxLength(25)
                .IsUnicode(false)
                .HasColumnName("TipoAutor");
        });

        ModelBuilder.Entity<TipoLibro>(entity =>
        {
            entity.HasKey(e => e.TipoLibroId).HasName("PK__TipoLibr__D5FDC1D54D51CEFE");

            entity.ToTable("TipoLibro");

            entity.Property(e => e.TipoLibroId).HasColumnName("TipoLibroID");
            entity.Property(e => e.TipoLibro1)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("TipoLibro");
        });

        ModelBuilder.Entity<TipoUsuario>(entity =>
        {
            entity.HasKey(e => e.TipousuarioId).HasName("PK__TipoUsua__3FA4F4009D6C1B62");

            entity.ToTable("TipoUsuario");

            entity.Property(e => e.Tipousuario)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("tipousuario");
        });

        ModelBuilder.Entity<Usuario>(entity =>
        {
            entity.HasKey(e => e.UsuarioId).HasName("PK__Usuario__2B3DE7B807425F12");

            entity.ToTable("Usuario");

            entity.Property(e => e.Pwsd)
                .HasColumnType("text")
                .HasColumnName("pwsd");
            entity.Property(e => e.Usu)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("usu");

            entity.HasOne(d => d.Tipousuario).WithMany(p => p.Usuarios)
                .HasForeignKey(d => d.TipousuarioId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Usuario__Tipousu__5535A963");
        });

        ModelBuilder.Entity<VAutor>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("v_autor");

            entity.Property(e => e.AutorId).HasColumnName("AutorID");
            entity.Property(e => e.NombreAutor)
                .HasMaxLength(80)
                .IsUnicode(false);
            entity.Property(e => e.TipoAutor)
                .HasMaxLength(25)
                .IsUnicode(false);
            entity.Property(e => e.TipoAutorId).HasColumnName("TipoAutorID");
        });

        ModelBuilder.Entity<VInvReporte>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("v_inv_reporte");

            entity.Property(e => e.Autenticidad)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("autenticidad");
            entity.Property(e => e.Autores)
                .HasMaxLength(8000)
                .IsUnicode(false);
            entity.Property(e => e.Codigo)
                .HasMaxLength(40)
                .IsUnicode(false);
            entity.Property(e => e.Descripcion)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.Editorial)
                .HasMaxLength(80)
                .IsUnicode(false);
            entity.Property(e => e.LibroId).HasColumnName("LibroID");
            entity.Property(e => e.NombreLib)
                .HasMaxLength(80)
                .IsUnicode(false);
            entity.Property(e => e.Valor).HasColumnName("valor");
        });

        ModelBuilder.Entity<VInventario>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("v_inventario");

            entity.Property(e => e.Autenticidad)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("autenticidad");
            entity.Property(e => e.AutenticidadId).HasColumnName("autenticidadID");
            entity.Property(e => e.Codigo)
                .HasMaxLength(40)
                .IsUnicode(false);
            entity.Property(e => e.Color)
                .HasMaxLength(7)
                .IsUnicode(false)
                .HasColumnName("color");
            entity.Property(e => e.Descripcion)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.EstadoId).HasColumnName("EstadoID");
            entity.Property(e => e.InventarioId).HasColumnName("InventarioID");
            entity.Property(e => e.LibroId).HasColumnName("LibroID");
            entity.Property(e => e.Valor).HasColumnName("valor");
        });

        ModelBuilder.Entity<VLibro>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("v_libro");

            entity.Property(e => e.AutorId).HasColumnName("AutorID");
            entity.Property(e => e.Editorial)
                .HasMaxLength(80)
                .IsUnicode(false);
            entity.Property(e => e.LibroId).HasColumnName("LibroID");
            entity.Property(e => e.NombreAutor)
                .HasMaxLength(80)
                .IsUnicode(false);
            entity.Property(e => e.NombreLib)
                .HasMaxLength(80)
                .IsUnicode(false);
            entity.Property(e => e.TipoLibro)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.TipoLibroId).HasColumnName("TipoLibroID");
        });

        OnModelCreatingPartial(ModelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder ModelsBuilder);
}
