using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProductStoringApp.DAL.DAO;
using ProductStoringApp.DAL.Gateway;

namespace ProductStoringApp.BLL
{
    class ProductBll
    {
        private ProductGateway aProductGateway;

        public ProductBll()
        {
            aProductGateway = new ProductGateway();
        }

        public string Save(Product aProduct)
        {
            if (HasThisProductCodeStored(aProduct.Code) || HasThisProductNameStored(aProduct.Name))
            {
                string msg = "";
                if (HasThisProductCodeStored(aProduct.Code))
                {
                    msg +="Product code already stored\n";
                }
                if (HasThisProductNameStored(aProduct.Name))
                {
                    msg +="Product name already stored";
                }
                return msg;
            }
            if (!(IsThisProductCodeThreeCharacter(aProduct.Code)) || !(IsThisProductNameFiveToTenCharacter(aProduct.Name)))
            {
                string msg = "";
                if (!IsThisProductCodeThreeCharacter(aProduct.Code))
                {
                    msg+= "Code length must be three character\n";
                }

                if (!IsThisProductNameFiveToTenCharacter(aProduct.Name))
                {
                    msg+= "Name length must be between 5 to 10 Character";
                }
                return msg;
            }
           
            return aProductGateway.Save(aProduct);
        }

        private bool IsThisProductNameFiveToTenCharacter(string name)
        {
            if (name.Length > 5 && name.Length < 11)
            {
                return true;
            }
            return false;
        }

        private bool IsThisProductCodeThreeCharacter(string code)
        {
            if (code.Length > 1 && code.Length < 4)
            {
                return true;
            }
            return false;
        }

        private bool HasThisProductNameStored(string name)
        {
            return aProductGateway.HasThisProductNameStored(name);
        }

        private bool HasThisProductCodeStored(string code)
        {
            return aProductGateway.HasThisProductCodeStored(code);
        }

        public List<Product> GetAllProduct(Product aProduct)
        {
            return aProductGateway.GetAllProduct(aProduct);
        }

        public int GetTotalQuantity()
        {
            return aProductGateway.GetTotalQuantity();
        }
    }
}
