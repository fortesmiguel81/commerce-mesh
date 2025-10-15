using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OrderService.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "public");

            migrationBuilder.CreateTable(
                name: "orders",
                schema: "public",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    user_id = table.Column<Guid>(type: "uuid", nullable: false),
                    order_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    total_amount_amount = table.Column<decimal>(type: "numeric", nullable: false),
                    total_amount_currency = table.Column<string>(type: "text", nullable: false),
                    status = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    priority = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    created_on_utc = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    confirmed_on_utc = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    processing_on_utc = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    shipped_on_utc = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    delivered_on_utc = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    cancelled_on_utc = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    returned_on_utc = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    refunded_on_utc = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    failed_on_utc = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    on_hold_on_utc = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_orders", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "orderItems",
                schema: "public",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    product_id = table.Column<Guid>(type: "uuid", nullable: false),
                    product_reference = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    unit_price_amount = table.Column<decimal>(type: "numeric", nullable: false),
                    unit_price_currency = table.Column<string>(type: "text", nullable: false),
                    quantity = table.Column<int>(type: "integer", nullable: false),
                    order_id = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_order_items", x => x.id);
                    table.ForeignKey(
                        name: "fk_order_items_orders_order_id",
                        column: x => x.order_id,
                        principalSchema: "public",
                        principalTable: "orders",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "ix_order_items_order_id",
                schema: "public",
                table: "orderItems",
                column: "order_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "orderItems",
                schema: "public");

            migrationBuilder.DropTable(
                name: "orders",
                schema: "public");
        }
    }
}
