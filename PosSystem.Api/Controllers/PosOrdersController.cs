using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PosSystem.Api.Controllers
{
    [ApiController]
    [Route("api/v1/pos/orders")]
    public class PosOrdersController : ControllerBase
    {
        [HttpPost]
        public async Task<IActionResult> CreateOrder([FromBody] OrderDto order)
        {
            if (order == null || order.Items == null || order.Items.Count == 0)
            {
                return BadRequest(new { message = "Order must contain at least one item." });
            }

            // In production:
            // 1. Validate tenant & store
            // 2. Decrement inventory atomically via Redis lock / DB transaction
            // 3. Persist order & payments to PostgreSQL
            // 4. Emit SignalR notification to Admin Web dashboard

            return Ok(new
            {
                status = "COMPLETED",
                orderId = order.Id,
                orderNumber = order.OrderNumber,
                syncedAt = DateTime.UtcNow
            });
        }

        [HttpPost("sync-offline")]
        public async Task<IActionResult> SyncOfflineOrder([FromBody] OrderDto order)
        {
            // Idempotent offline reconciliation:
            // If order already exists by client GUID, return 200 OK directly.
            return Ok(new
            {
                status = "SYNCED",
                orderId = order.Id,
                reconciled = true,
                syncedAt = DateTime.UtcNow
            });
        }
    }

    public class OrderDto
    {
        public string Id { get; set; } = string.Empty;
        public string OrderNumber { get; set; } = string.Empty;
        public string TenantId { get; set; } = string.Empty;
        public string StoreId { get; set; } = string.Empty;
        public string TerminalId { get; set; } = string.Empty;
        public string CashierId { get; set; } = string.Empty;
        public string CashierName { get; set; } = string.Empty;
        public string? CustomerName { get; set; }
        public decimal Subtotal { get; set; }
        public decimal DiscountTotal { get; set; }
        public decimal TaxTotal { get; set; }
        public decimal GrandTotal { get; set; }
        public List<OrderItemDto> Items { get; set; } = new();
    }

    public class OrderItemDto
    {
        public string ProductId { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public string Sku { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public int Quantity { get; set; }
        public decimal TaxAmount { get; set; }
        public decimal Total { get; set; }
    }
}
