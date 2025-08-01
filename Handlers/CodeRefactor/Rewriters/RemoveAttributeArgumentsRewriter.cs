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
    class RemoveAttributeArgumentsRewriter : CSharpSyntaxRewriter
    {
        private string _attributeName;

        public RemoveAttributeArgumentsRewriter(string attributeName)
        {
            _attributeName = attributeName;
        }

        public override SyntaxNode? VisitAttribute(AttributeSyntax node)
        {
            if (string.Equals(node.Name.ToString(), _attributeName, StringComparison.OrdinalIgnoreCase)
                && node.ArgumentList != null)
            {
                return node.WithArgumentList(null);
            }

            return base.VisitAttribute(node);
        }

    }
}
