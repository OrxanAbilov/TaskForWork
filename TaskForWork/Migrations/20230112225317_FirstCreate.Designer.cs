// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using TaskForWork.Data;

#nullable disable

namespace TaskForWork.Migrations
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20230112225317_FirstCreate")]
    partial class FirstCreate
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.2")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("TaskForWork.Models.Invoice", b =>
                {
                    b.Property<int>("InvId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("InvId"));

                    b.Property<double>("Amount")
                        .HasColumnType("float");

                    b.Property<int>("ItemId")
                        .HasColumnType("int");

                    b.Property<double>("Price")
                        .HasColumnType("float");

                    b.Property<double>("Total")
                        .HasColumnType("float");

                    b.HasKey("InvId");

                    b.HasIndex("ItemId");

                    b.ToTable("Invoices");
                });

            modelBuilder.Entity("TaskForWork.Models.Item", b =>
                {
                    b.Property<int>("ItemId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ItemId"));

                    b.Property<string>("ItemName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<double>("ItemPrice")
                        .HasColumnType("float");

                    b.Property<double>("ItemStock")
                        .HasColumnType("float");

                    b.HasKey("ItemId");

                    b.ToTable("Items");
                });

            modelBuilder.Entity("TaskForWork.Models.Invoice", b =>
                {
                    b.HasOne("TaskForWork.Models.Item", "Item")
                        .WithMany()
                        .HasForeignKey("ItemId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Item");
                });
#pragma warning restore 612, 618
        }
    }
}
