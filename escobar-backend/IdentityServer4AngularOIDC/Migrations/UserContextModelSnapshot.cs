﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Server.Models;

namespace Server.Migrations
{
    [DbContext(typeof(UserContext))]
    partial class UserContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.1.8-servicing-32085")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Server.Models.Comando", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ComandoName");

                    b.Property<string>("Modulo");

                    b.Property<string>("Sistema");

                    b.HasKey("Id");

                    b.ToTable("Comandos");
                });

            modelBuilder.Entity("Server.Models.User", b =>
                {
                    b.Property<Guid>("ID")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("CPF");

                    b.Property<string>("Email");

                    b.Property<string>("Login");

                    b.Property<string>("Nome");

                    b.Property<string>("Senha");

                    b.Property<string>("SenhaHash");

                    b.HasKey("ID");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("Server.Models.UserAccess", b =>
                {
                    b.Property<Guid>("ID")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("DataAccess");

                    b.Property<string>("Log");

                    b.Property<bool>("Success");

                    b.Property<Guid>("UserID");

                    b.HasKey("ID");

                    b.ToTable("UserAccesses");
                });

            modelBuilder.Entity("Server.Models.UserComandos", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<Guid>("ComandoId");

                    b.Property<Guid>("UserId");

                    b.HasKey("Id");

                    b.ToTable("UserComandos");
                });
#pragma warning restore 612, 618
        }
    }
}
