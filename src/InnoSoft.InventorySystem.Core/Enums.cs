using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public enum Roles
{
    Admin = 1,
    EndUser = 2
}

public enum Permissions
{
    CreateProduct = 1,
    UpdateProduct = 2,
    DeleteProduct = 3,
    ViewProduct = 4,
    AddQuantityToProduct = 5,
    RemoveQuantityFromProduct = 6,
    CreateCategory = 7,
    UpdateCategory = 8,
    DeleteCategory = 9,
    ViewCategory = 10,
}
