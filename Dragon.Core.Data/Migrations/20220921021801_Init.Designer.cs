// <auto-generated />
using System;
using Dragon.Core.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Dragon.Core.Data.Migrations
{
    [DbContext(typeof(EfDbContext))]
    [Migration("20220921021801_Init")]
    partial class Init
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "6.0.8");

            modelBuilder.Entity("Dragon.Core.Entity.ApiModule", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("ApiName")
                        .IsRequired()
                        .HasMaxLength(500)
                        .HasColumnType("TEXT");

                    b.Property<string>("ApiUrl")
                        .IsRequired()
                        .HasMaxLength(500)
                        .HasColumnType("TEXT");

                    b.Property<int>("CreatedId")
                        .HasColumnType("INTEGER");

                    b.Property<string>("CreatedName")
                        .HasColumnType("TEXT");

                    b.Property<DateTime?>("CreatedTime")
                        .HasColumnType("TEXT");

                    b.Property<bool?>("IsDrop")
                        .HasColumnType("INTEGER");

                    b.Property<int>("OrderNo")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Remark")
                        .HasColumnType("TEXT");

                    b.Property<int>("Status")
                        .HasColumnType("INTEGER");

                    b.Property<int>("UpdateId")
                        .HasColumnType("INTEGER");

                    b.Property<string>("UpdateName")
                        .HasColumnType("TEXT");

                    b.Property<DateTime?>("UpdateTime")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("ApiModule");
                });

            modelBuilder.Entity("Dragon.Core.Entity.SysDepartMent", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Code")
                        .HasMaxLength(50)
                        .HasColumnType("TEXT");

                    b.Property<int>("CreatedId")
                        .HasColumnType("INTEGER");

                    b.Property<string>("CreatedName")
                        .HasColumnType("TEXT");

                    b.Property<DateTime?>("CreatedTime")
                        .HasColumnType("TEXT");

                    b.Property<bool?>("IsDrop")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("TEXT");

                    b.Property<int>("OrderNo")
                        .HasColumnType("INTEGER");

                    b.Property<int>("Pid")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Remark")
                        .HasColumnType("TEXT");

                    b.Property<int>("Status")
                        .HasColumnType("INTEGER");

                    b.Property<int>("UpdateId")
                        .HasColumnType("INTEGER");

                    b.Property<string>("UpdateName")
                        .HasColumnType("TEXT");

                    b.Property<DateTime?>("UpdateTime")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("Name");

                    b.ToTable("SysDepartMent");
                });

            modelBuilder.Entity("Dragon.Core.Entity.SysMenu", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Component")
                        .HasMaxLength(200)
                        .HasColumnType("TEXT");

                    b.Property<int>("CreatedId")
                        .HasColumnType("INTEGER");

                    b.Property<string>("CreatedName")
                        .HasColumnType("TEXT");

                    b.Property<DateTime?>("CreatedTime")
                        .HasColumnType("TEXT");

                    b.Property<string>("CurrentActiveMenu")
                        .HasMaxLength(200)
                        .HasColumnType("TEXT");

                    b.Property<string>("FrameSrc")
                        .HasMaxLength(500)
                        .HasColumnType("TEXT");

                    b.Property<bool?>("HideMenu")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Icon")
                        .HasMaxLength(200)
                        .HasColumnType("TEXT");

                    b.Property<bool?>("IgnoreKeepAlive")
                        .HasColumnType("INTEGER");

                    b.Property<bool?>("IsDrop")
                        .HasColumnType("INTEGER");

                    b.Property<int>("MenuType")
                        .HasColumnType("INTEGER");

                    b.Property<int>("Mid")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("TEXT");

                    b.Property<int>("OrderNo")
                        .HasColumnType("INTEGER");

                    b.Property<int>("ParId")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Permission")
                        .HasMaxLength(200)
                        .HasColumnType("TEXT");

                    b.Property<string>("Redirect")
                        .HasMaxLength(200)
                        .HasColumnType("TEXT");

                    b.Property<string>("Remark")
                        .HasColumnType("TEXT");

                    b.Property<int>("Status")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("TEXT");

                    b.Property<int>("UpdateId")
                        .HasColumnType("INTEGER");

                    b.Property<string>("UpdateName")
                        .HasColumnType("TEXT");

                    b.Property<DateTime?>("UpdateTime")
                        .HasColumnType("TEXT");

                    b.Property<string>("path")
                        .IsRequired()
                        .HasMaxLength(500)
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("SysMenu");
                });

            modelBuilder.Entity("Dragon.Core.Entity.SysRole", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Code")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("TEXT");

                    b.Property<int>("CreatedId")
                        .HasColumnType("INTEGER");

                    b.Property<string>("CreatedName")
                        .HasColumnType("TEXT");

                    b.Property<DateTime?>("CreatedTime")
                        .HasColumnType("TEXT");

                    b.Property<int>("DataScope")
                        .HasColumnType("INTEGER");

                    b.Property<bool?>("IsDrop")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(256)
                        .HasColumnType("TEXT");

                    b.Property<int>("OrderNo")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Remark")
                        .HasMaxLength(200)
                        .HasColumnType("TEXT");

                    b.Property<int>("Status")
                        .HasColumnType("INTEGER");

                    b.Property<int>("UpdateId")
                        .HasColumnType("INTEGER");

                    b.Property<string>("UpdateName")
                        .HasColumnType("TEXT");

                    b.Property<DateTime?>("UpdateTime")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("Name");

                    b.ToTable("SysRole");
                });

            modelBuilder.Entity("Dragon.Core.Entity.SysRoleMenuModule", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("CreatedId")
                        .HasColumnType("INTEGER");

                    b.Property<string>("CreatedName")
                        .HasColumnType("TEXT");

                    b.Property<DateTime?>("CreatedTime")
                        .HasColumnType("TEXT");

                    b.Property<bool?>("IsDrop")
                        .HasColumnType("INTEGER");

                    b.Property<int>("MenuId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("Mid")
                        .HasColumnType("INTEGER");

                    b.Property<int>("OrderNo")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Remark")
                        .HasColumnType("TEXT");

                    b.Property<int>("RoleId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("Status")
                        .HasColumnType("INTEGER");

                    b.Property<int>("UpdateId")
                        .HasColumnType("INTEGER");

                    b.Property<string>("UpdateName")
                        .HasColumnType("TEXT");

                    b.Property<DateTime?>("UpdateTime")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("SysRoleMenuModule");
                });

            modelBuilder.Entity("Dragon.Core.Entity.SysUser", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Account")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("TEXT");

                    b.Property<string>("Avater")
                        .HasMaxLength(200)
                        .HasColumnType("TEXT");

                    b.Property<int>("CreatedId")
                        .HasColumnType("INTEGER");

                    b.Property<string>("CreatedName")
                        .HasColumnType("TEXT");

                    b.Property<DateTime?>("CreatedTime")
                        .HasColumnType("TEXT");

                    b.Property<int>("DepartmentId")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Email")
                        .HasMaxLength(200)
                        .HasColumnType("TEXT");

                    b.Property<int>("EmpliyeeType")
                        .HasColumnType("INTEGER");

                    b.Property<int>("ErrorCount")
                        .HasColumnType("INTEGER");

                    b.Property<string>("HistroyPsw")
                        .HasMaxLength(200)
                        .HasColumnType("TEXT");

                    b.Property<bool?>("IsDrop")
                        .HasColumnType("INTEGER");

                    b.Property<string>("JobName")
                        .HasMaxLength(200)
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("LastErrTime")
                        .HasColumnType("TEXT");

                    b.Property<string>("Mobile")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("TEXT");

                    b.Property<int>("OrderNo")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("TEXT");

                    b.Property<string>("Remark")
                        .HasMaxLength(200)
                        .HasColumnType("TEXT");

                    b.Property<bool>("Sex")
                        .HasColumnType("INTEGER");

                    b.Property<int>("Status")
                        .HasColumnType("INTEGER");

                    b.Property<int>("UpdateId")
                        .HasColumnType("INTEGER");

                    b.Property<string>("UpdateName")
                        .HasColumnType("TEXT");

                    b.Property<DateTime?>("UpdateTime")
                        .HasColumnType("TEXT");

                    b.Property<string>("WeChat")
                        .HasMaxLength(200)
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("Name");

                    b.ToTable("SysUser");
                });

            modelBuilder.Entity("Dragon.Core.Entity.SysUserDepartMent", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("CreatedId")
                        .HasColumnType("INTEGER");

                    b.Property<string>("CreatedName")
                        .HasColumnType("TEXT");

                    b.Property<DateTime?>("CreatedTime")
                        .HasColumnType("TEXT");

                    b.Property<int>("DepartMentId")
                        .HasColumnType("INTEGER");

                    b.Property<bool?>("IsDrop")
                        .HasColumnType("INTEGER");

                    b.Property<int>("OrderNo")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Remark")
                        .HasColumnType("TEXT");

                    b.Property<int>("Status")
                        .HasColumnType("INTEGER");

                    b.Property<int>("UpdateId")
                        .HasColumnType("INTEGER");

                    b.Property<string>("UpdateName")
                        .HasColumnType("TEXT");

                    b.Property<DateTime?>("UpdateTime")
                        .HasColumnType("TEXT");

                    b.Property<int>("UserId")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.ToTable("SysUserDepartMent");
                });

            modelBuilder.Entity("Dragon.Core.Entity.SysUserRole", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("CreatedId")
                        .HasColumnType("INTEGER");

                    b.Property<string>("CreatedName")
                        .HasColumnType("TEXT");

                    b.Property<DateTime?>("CreatedTime")
                        .HasColumnType("TEXT");

                    b.Property<bool?>("IsDrop")
                        .HasColumnType("INTEGER");

                    b.Property<int>("OrderNo")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Remark")
                        .HasColumnType("TEXT");

                    b.Property<int>("RoleId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("Status")
                        .HasColumnType("INTEGER");

                    b.Property<int>("UpdateId")
                        .HasColumnType("INTEGER");

                    b.Property<string>("UpdateName")
                        .HasColumnType("TEXT");

                    b.Property<DateTime?>("UpdateTime")
                        .HasColumnType("TEXT");

                    b.Property<int>("UserId")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.ToTable("SysUserRole");
                });
#pragma warning restore 612, 618
        }
    }
}
