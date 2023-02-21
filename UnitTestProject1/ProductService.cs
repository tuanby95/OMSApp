using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using OMSTest;

namespace UnitTestProject1
{
    public static class ProductService
    {
        internal static List<Product> GetProducts(int channelId, string productStatus = "")
        {


            string byProductStatus = productStatus.Length > 0 ? string.Format(@"AND ProductChannelStatus = '{0}'", productStatus) : string.Empty;

            var sql = SqlQueryHelper.GetProductsQuery(channelId, byProductStatus);

            var reader = SqlHelper.ExecuteReader(sql, CommandType.Text);
            var result = new List<Product>();

            while (reader.Read())
            {
                result.Add(new Product
                {
                    ProductId = Convert.ToInt32(reader[0]),
                    ProductVariantImage = (byte[])reader[1],
                    ProductDescription = Convert.ToString(reader[2]),
                    ProductName = Convert.ToString(reader[3]),
                    ProductVariantValue = Convert.ToString(reader[4]),
                    LastUpdatedAt = Convert.ToDateTime(reader[5]),
                    Quantity = Convert.ToInt32(reader[6]),
                    CSKU = Convert.ToString(reader[7]),
                    Price = Convert.ToInt32(reader[8]),
                    ProductChannelStatus = Convert.ToString(reader[9])
                });
            }

            return result;
        }

        internal static int UpdateProductStock(int channelId, int productId, int warehouseId, int updateStockQuantity)
        {
            var sql = SqlQueryHelper.UpdateProductStockQuery(channelId, productId, warehouseId, updateStockQuantity);

            var result = SqlHelper.ExecuteNonQuery(sql, CommandType.Text);
            return result;
        }
        internal static int InsertNewProduct(Product product)
        {
            var sql = SqlQueryHelper.InsertNewProductQuery(product);

            var result = SqlHelper.ExecuteNonQuery(sql, CommandType.Text);

            return result;
        }
        internal static int InsertProductChannelStock(int productId, int channelId, int updateStock, string status = "ACTIVE")
        {
            var sql = SqlQueryHelper.InserProductChannelStockQuery(productId, channelId, updateStock, status);

            return SqlHelper.ExecuteNonQuery(sql, CommandType.Text);
        }

        internal static List<Product> GetProductsDapper(int channelId, string productStatus = "")
        {
            string byProductStatus = productStatus.Length > 0 ? string.Format(@"AND ProductChannelStatus = '{0}'", productStatus) : string.Empty;

            var sql = SqlQueryHelper.GetProductsQuery(channelId, byProductStatus);

            return SqlHelper.Query<Product>(sql);
        }

        internal static int UpdateProductStockDapper(int channelId, int productVariantId, int warehouseId, int updateStockQuantity)
        {
            var sql = SqlQueryHelper.UpdateProductStockQuery(channelId, productVariantId, warehouseId, updateStockQuantity);
            return SqlHelper.Excecute(sql);
        }
    }
}
