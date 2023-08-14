﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Sistema_Inventario.Datos;

#nullable disable

namespace Sistema_Inventario.Migrations
{
    [DbContext(typeof(ApplicationDbContext.ApplicationDbContexto))]
    [Migration("20230814053544_InitialCreate")]
    partial class InitialCreate
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.10")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Sistema_Inventario.Models.CategoriaModel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Descripcion")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("Estado")
                        .HasColumnType("bit");

                    b.Property<string>("Nombre")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("categoriaModels");
                });

            modelBuilder.Entity("Sistema_Inventario.Models.ProductoModel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("CantidadActual")
                        .HasColumnType("int");

                    b.Property<int>("CategoriaId")
                        .HasColumnType("int");

                    b.Property<string>("CodigoProducto")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<float>("Costo")
                        .HasColumnType("real");

                    b.Property<float>("Descuento")
                        .HasColumnType("real");

                    b.Property<bool>("Estado")
                        .HasColumnType("bit");

                    b.Property<decimal>("Impuesto")
                        .HasColumnType("decimal(18,2)");

                    b.Property<string>("NombreProducto")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<float>("PrecioVenta")
                        .HasColumnType("real");

                    b.Property<float>("Utilidad")
                        .HasColumnType("real");

                    b.HasKey("Id");

                    b.HasIndex("CategoriaId");

                    b.ToTable("productoModels");
                });

            modelBuilder.Entity("Sistema_Inventario.Models.ProductoModel", b =>
                {
                    b.HasOne("Sistema_Inventario.Models.CategoriaModel", "Categoria")
                        .WithMany("Productos")
                        .HasForeignKey("CategoriaId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Categoria");
                });

            modelBuilder.Entity("Sistema_Inventario.Models.CategoriaModel", b =>
                {
                    b.Navigation("Productos");
                });
#pragma warning restore 612, 618
        }
    }
}