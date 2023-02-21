using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OMSTest;

namespace UnitTestProject1
{
    public static class ProductService
    {
        internal static List<ProductChannel> GetProductsByChannel(int channelId, string productStatus = "")
        {


            string byProductStatus = productStatus.Length > 0 ? string.Format(@"AND ProductChannelStatus = '{0}'", productStatus) : string.Empty;

            var sql = SqlQueryHelper.GetProductsByChannelQuery(channelId, byProductStatus);

            var reader = SqlHelper.ExecuteReader(sql, CommandType.Text);
            var result = new List<ProductChannel>();

            while (reader.Read())
            {
                result.Add(new ProductChannel
                {
                    ProductId = Convert.ToInt32(reader[0]),
                    ProductMainImg = Convert.ToString(reader[1]),
                    SKU = Convert.ToString(reader[2]),
                    ProductName = Convert.ToString(reader[3]),
                    CreatedAt = Convert.ToDateTime(reader[4]),
                    UpdatedAt = Convert.ToDateTime(reader[5]),
                    AvaiableStock = Convert.ToInt32(reader[6]),
                    Price = Convert.ToInt32(reader[7]),
                    ProductChannelStatus = Convert.ToString(reader[8])
                });
            }

            return result;
        }

        internal static int UpdateProductStockByChannel(int channelId, int productId, int updateStockQuantity)
        {
            var sql = SqlQueryHelper.UpdateProductStockByChannelQuery(channelId, productId, updateStockQuantity);

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

            var result = SqlHelper.ExecuteNonQuery(sql, CommandType.Text);

            return result;
        }
    }
}
