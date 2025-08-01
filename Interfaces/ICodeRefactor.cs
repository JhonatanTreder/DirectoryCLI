using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DirectoryCLI.Interfaces
{
    interface ICodeRefactor
    {
        //D:/Projetos/ProductsAPI search "[Authorize]" list-properties
        //D:/Projetos/ProductsAPI/Controllers search "[Authorize]" list-properties "AuthController.cs"
        //D:/Projetos/ProductsAPI search "[Authorize]" list-properties from-files "AuthController.cs"
        //D:/Projetos/ProductsAPI search "[Authorize]" list-properties from-dir "Controllers"
        //D:/Projetos/ProductsAPI search "[Authorize]" list-properties from-dir --this

        //D:/Projetos/ProductsAPI search "[Authorize]" remove-all-properties
        //D:/Projetos/ProductsAPI/Controllers search "[Authorize]" remove-all-properties "AuthControler.cs"
        //D:/Projetos/ProductsAPI search "[Authorize]" remove-all-properties from-files "ProductController.cs"
        //D:/Projetos/ProductsAPI search "[Authorize]" remove-all-properties from-dir "Controller"
        //D:/Projetos/ProductsAPI search "[Authorize]" remove-all-properties from-dir --this
        //---------------------------------------------------------------------------------------------------------


        //D:/Projetos/ProductsAPI search "[Authorize]" remove-prop "Roles"
        //D:/Projetos/ProductsAPI/Controllers search "[Authorize]" remove-prop "Roles" "AuthController.cs"
        //D:/Projetos/ProductsAPI search "[Authorize]" remove-prop "Roles" from-files "AuthController.cs"
        //D:/Projetos/ProductsAPI search "[Authorize]" remove-prop "Roles" from-dir "Controllers"
        //D:/Projetos/ProductsAPI search "[Authorize]" remove-prop "Roles" from-dir --this

        //D:/Projetos/ProductsAPI search "[Authorize]" add-prop "Roles"
        //D:/Projetos/ProductsAPI/Controllers search "[Authorize]" add-prop "Roles" "AuthController.cs"
        //D:/Projetos/ProductsAPI search "[Authorize]" add-prop "Roles" from-files "AuthController.cs"
        //D:/Projetos/ProductsAPI search "[Authorize]" add-prop "Roles" from-dir "Controllers"
        //D:/Projetos/ProductsAPI search "[Authorize]" add-prop "Roles" from-dir --this

        //D:/Projetos/ProductsAPI search "[Authorize]" add-prop "Roles" value "user,admin"
        //D:/Projetos/ProductsAPI/Controllers search "[Authorize]" add-prop "Roles" value "user,admin" "AuthController.cs"
        //D:/Projetos/ProductsAPI search "[Authorize]" add-prop "Roles" value "user,admin" from-files "AuthController.cs"
        //D:/Projetos/ProductsAPI search "[Authorize]" add-prop "Roles" value "user,admin" from-dir Controllers
        //D:/Projetos/ProductsAPI search "[Authorize]" add-prop "Roles" value "user,admin" from-dir --this

        //D:/Projetos/ProductsAPI search "[Authorize]" update-prop "Roles" new-value "admin"
        //D:/Projetos/ProductsAPI/Controllers search "[Authorize]" update-prop "Roles" new-value "admin" "AuthController.cs"
        //D:/Projetos/ProductsAPI search "[Authorize]" update-prop "Roles" new-value "admin" from-files "AuthController.cs"
        //D:/Projetos/ProductsAPI search "[Authorize]" update-prop "Roles" new-value "admin" from-dir Controllers
        //D:/Projetos/ProductsAPI search "[Authorize]" update-prop "Roles" new-value "admin" from-dir --this
        void DelegateFunction(string[] arguments);
        void ListAttributeProperties(string[] arguments);
        void RemoveAttributeProperties(string[] arguments);
        void AddAttributeProperty(string[] arguments);
        void ReplaceAnnotationAttribute(string[] arguments);
    }
}
