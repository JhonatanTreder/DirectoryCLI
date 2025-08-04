using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DirectoryCLI.Handlers.CodeRefactor.Rewriters
{
    public class RemoveSingleAttributePropertyRewriter : CSharpSyntaxRewriter
    {
        private string _attributeName;
        private string _propertyName;

        public RemoveSingleAttributePropertyRewriter(string attributeName, string propertyName)
        {
            _attributeName = attributeName;
            _propertyName = propertyName;
        }

        public override SyntaxNode? VisitAttribute(AttributeSyntax node)
        {

            if (string.Equals(node.Name.ToFullString(), _attributeName, StringComparison.OrdinalIgnoreCase)
                && node.ArgumentList != null)
            {
                var newArguments = node.ArgumentList.Arguments
                    .Where(argument => argument.NameEquals == null ||
                    !string.Equals(argument.NameEquals.Name.ToString(), _propertyName)).ToList();

                if (newArguments.Count != node.ArgumentList.Arguments.Count)
                {
                    var newArgumentList = SyntaxFactory
                        .AttributeArgumentList(SyntaxFactory
                        .SeparatedList(newArguments));

                    return node.WithArgumentList(newArgumentList);
                }
            }

            return base.VisitAttribute(node);
        }
    }
}
