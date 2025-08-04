using DirectoryCLI.Handlers.CodeRefactor.Rewriters;
using DirectoryCLI.Interfaces;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Spectre.Console;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Security;
using System.Text;
using System.Threading.Tasks;

namespace DirectoryCLI.Handlers
{
    class RoslynCodeRefactor : ICodeRefactor
    {
        public void DelegateFunction(string[] arguments)
        {
            switch (arguments[3])
            {
                case "remove-props":
                case "remove-all-props":

                    RemoveAttributeProperties(arguments);

                    break;

                case "add-prop":
                    AddAttributeProperty(arguments);
                    break;

                case "update-prop":
                    ReplaceAnnotationAttribute(arguments);
                    break;

                default:

                    ListAttributeProperties(arguments);

                    break;
            }
        }

        public void ListAttributeProperties(string[] arguments)
        {
            string dataAnnotation = ValidateString(arguments[2]);
            var filteredFiles = GenerateFilteredFiles(arguments);

            var filesWithAttributes = new Dictionary<string, List<string>>();

            foreach (string csFile in filteredFiles)
            {
                var code = File.ReadAllText(csFile);
                var tree = CSharpSyntaxTree.ParseText(code);
                var root = tree.GetCompilationUnitRoot();

                var attributeNodes = root.DescendantNodes().OfType<AttributeSyntax>();

                foreach (var attributeDeclaration in attributeNodes)
                {
                    string attributeName = attributeDeclaration.Name.ToFullString().Trim();
                    var argumentList = attributeDeclaration.ArgumentList;

                    if (attributeName.Equals(dataAnnotation, StringComparison.OrdinalIgnoreCase))
                    {
                        if (argumentList is null)
                        {
                            if (!filesWithAttributes.ContainsKey(csFile))
                                filesWithAttributes[csFile] = new List<string>();

                            filesWithAttributes[csFile]
                                .Add(attributeDeclaration
                                .ToFullString().Trim());
                        }

                        else
                        {
                            foreach (var value in argumentList.Arguments)
                            {
                                if (!filesWithAttributes.ContainsKey(csFile))
                                    filesWithAttributes[csFile] = new List<string>();

                                filesWithAttributes[csFile]
                                    .Add(attributeDeclaration.ToFullString().Trim());
                            }
                        }
                    }
                }
            }

            if (filesWithAttributes.Count >= 1)
            {

                foreach (var kvp in filesWithAttributes)
                {
                    Table dataAnnotationTable = new Table();

                    dataAnnotationTable.AddColumn(new TableColumn("Caminho do código fonte:")
                    { Width = 70 });

                    dataAnnotationTable.AddColumn(new TableColumn("Data Annotation:")
                    { Width = 40 });

                    foreach (var attr in kvp.Value.Distinct())
                    {
                        dataAnnotationTable
                            .AddRow($"{Markup.Escape($"{kvp.Key}")}",
                                    $"{Markup.Escape($"[{attr}]")}");
                    }

                    AnsiConsole.Write(dataAnnotationTable);
                    AnsiConsole.WriteLine();
                }
            }

            else
            {
                Table filesTable = new();

                filesTable.AddColumn("Nenhum Data Annotation foi encontrado");

                AnsiConsole.Write(filesTable);
                AnsiConsole.WriteLine();
            }
        }

        public void RemoveAttributeProperties(string[] arguments)
        {
            //D:/Projetos/ProductsAPI search "[Authorize]" --remove-all-properties
            //D:/Projetos/ProductsAPI search "[Authorize]" --remove-all-properties "Program.cs"
            //D:/Projetos/ProductsAPI search "[Authorize]" --remove-all-properties --from-files "ProductController.cs"
            //D:/Projetos/ProductsAPI search "[Authorize]" --remove-all-properties --from-dir "Controller"
            //D:/Projetos/ProductsAPI search "[Authorize]" --remove-all-properties --from-dir --this

            //D:/Projetos/ProductsAPI search "[Authorize]" --remove-prop "Roles"
            //D:/Projetos/ProductsAPI search "[Authorize]" --remove-prop "Roles" "Program.cs"
            //D:/Projetos/ProductsAPI search "[Authorize]" --remove-prop "Roles" --from-files "AuthController.cs"
            //D:/Projetos/ProductsAPI search "[Authorize]" --remove-prop "Roles" --from-dir "Controller"
            //D:/Projetos/ProductsAPI search "[Authorize]" --remove-prop "Roles" --from-dir --this

            var parameter = arguments[3];
            var dataAnnotation = ValidateString(arguments[2]);
            var propertyName = ValidateString(arguments[4]);
            var filteredFiles = GenerateFilteredFiles(arguments);

            foreach (var csFile in filteredFiles)
            {
                var code = File.ReadAllText(csFile);
                var tree = CSharpSyntaxTree.ParseText(code);
                var root = tree.GetCompilationUnitRoot();

                switch (parameter)
                {
                    case "remove-props":

                        var singleAttributeRewriter = new RemoveSingleAttributePropertyRewriter(dataAnnotation,
                            propertyName);

                        CompilationUnitSyntax singleAttributeRoot = (CompilationUnitSyntax)singleAttributeRewriter
                            .Visit(root);

                        if (singleAttributeRoot.ToFullString() != root.ToFullString())
                        {
                            File.WriteAllText(csFile, singleAttributeRoot.ToFullString());
                        }

                        break;

                    case "remove-all-props":

                        RemoveAttributeArgumentsRewriter AttributesRewriter = new(dataAnnotation);
                        CompilationUnitSyntax attributesRoot = (CompilationUnitSyntax)AttributesRewriter.Visit(root);

                        if (attributesRoot.ToFullString() != root.ToFullString())
                        {
                            File.WriteAllText(csFile, attributesRoot.ToFullString());
                        }

                        break;
                        //FAZER VERIFICAÇÃO DE COMANDOS DEPOIS
                }
            }
        }

        public void AddAttributeProperty(string[] arguments)
        {
            //D:/Projetos/ProductsAPI search "[Authorize]" add-prop "Roles" value "user,admin"
            //D:/Projetos/ProductsAPI/Controllers search "[Authorize]" add-prop "Roles" value "user,admin" "ProductsController.cs"
            //D:/Projetos/ProductsAPI search "[Authorize]" add-prop "Roles" value "user,admin" from-files "AuthController.cs"
            //D:/Projetos/ProductsAPI search "[Authorize]" add-prop "Roles" value "user,admin" from-dir Controllers

            var dataAnnotation = ValidateString(arguments[2]);
            var propertyName = ValidateString(arguments[4]);
            var propertyValue = arguments[6].Replace("\"", "");
            var filteredFiles = GenerateFilteredFiles(arguments);

            foreach (var csFile in filteredFiles)
            {
                var code = File.ReadAllText(csFile);
                var tree = CSharpSyntaxTree.ParseText(code);
                var root = tree.GetCompilationUnitRoot();

                var rewriter = new AddAttributePropertyRewriter(dataAnnotation, propertyName, propertyValue);
                CompilationUnitSyntax newRoot = (CompilationUnitSyntax)rewriter.Visit(root);

                if (newRoot.ToFullString() != root.ToFullString())
                {
                    File.WriteAllText(csFile, newRoot.ToFullString());
                }
            }
        }

        public void ReplaceAnnotationAttribute(string[] arguments)
        {
            var dataAnnotation = ValidateString(arguments[2]);
            var propertyName = arguments[4].Replace("\"","");
            var newPropertyValue = arguments[6].Replace("\"","");
            var filteredFiles = GenerateFilteredFiles(arguments);

            foreach (var csFile in filteredFiles)
            {
                var code = File.ReadAllText(csFile);
                var tree = CSharpSyntaxTree.ParseText(code);
                var root = tree.GetCompilationUnitRoot();
                var rewriter = new ReplaceAttributeValueRewriter(dataAnnotation, propertyName, newPropertyValue);
                CompilationUnitSyntax newRoot = (CompilationUnitSyntax)rewriter.Visit(root);

                if (newRoot.ToFullString() != root.ToFullString())
                {
                    File.WriteAllText(csFile, newRoot.ToFullString());
                }
            }
        }

        private string ValidateString(string dataAnnotation)
        {
            dataAnnotation = dataAnnotation.Replace("\"[", string.Empty)
                                           .Replace("]\"", string.Empty)
                                           .Replace("[", string.Empty)
                                           .Replace("]", string.Empty)
                                           .Replace("\"", string.Empty);

            string newValue = char.ToUpper(dataAnnotation[0]) + dataAnnotation.Substring(1);

            return newValue;
        }

        private List<string> GenerateFilteredFiles(string[] arguments)
        {
            string projectPath = arguments[0];
            List<string> filteredFiles = new();

            if (arguments.Length <= 4)
                return GetAllFiles(projectPath);

            string secondArg = arguments[^2];
            string finalValue = arguments[^1];

            if (secondArg.Equals("from-files", StringComparison.OrdinalIgnoreCase))
            {
                return GetAllEspecificFiles(projectPath, CleanQuotes(finalValue));
            }

            else if (secondArg.Equals("from-dir", StringComparison.OrdinalIgnoreCase))
            {
                return GetAllFilesFromEspecificDir(projectPath, CleanQuotes(finalValue));
            }

            if (arguments.Length == 5 || arguments.Length == 8)
            {
                string last = arguments[^1].Replace("\"", "");

                if (last.EndsWith(".cs", StringComparison.OrdinalIgnoreCase))
                {
                    return new List<string>()
                    {
                        Path.Combine(projectPath, CleanQuotes(last))
                    };
                }
            }

            return GetAllFiles(projectPath);
        }

        private string CleanQuotes(string value)
        {
            return value.Replace("\"", "").Trim();
        }

        private List<string> GetAllFiles(string dir)
        {
            return Directory.GetFiles(dir, "*.cs", SearchOption.AllDirectories).ToList();
        }

        private List<string> GetAllEspecificFiles(string dir, string fileName)
        {
            List<string> filteredFiles = new();

            foreach (var csFile in Directory.GetFiles(dir, "*.cs", SearchOption.AllDirectories))
            {
                string file = Path.GetFileName(csFile);

                if (string.Equals(file, fileName, StringComparison.OrdinalIgnoreCase))
                {
                    filteredFiles.Add(csFile);
                }
            }

            return filteredFiles;
        }

        private List<string> GetAllFilesFromEspecificDir(string dir, string targetDir)
        {
            List<string> filteredFiles = new();

            if (targetDir == "--this")
            {
                foreach (var csFile in Directory.GetFiles(dir, "*.cs"))
                {
                    filteredFiles.Add(csFile);
                }

                return filteredFiles;
            }

            foreach (var directory in Directory.GetDirectories(dir, "*", SearchOption.AllDirectories))
            {
                if (Path.GetFileName(directory).Equals(targetDir, StringComparison.OrdinalIgnoreCase))
                {
                    foreach (var csFile in Directory.GetFiles(directory, "*.cs", SearchOption.AllDirectories))
                    {
                        filteredFiles.Add(csFile);
                    }
                }
            }

            return filteredFiles;
        }
    }
}
