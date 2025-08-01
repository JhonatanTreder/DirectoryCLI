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
    public class AddAttributePropertyRewriter : CSharpSyntaxRewriter
    {
        private string _attributeName;
        private string _propertyName;
        private string _argumentValue;

        public AddAttributePropertyRewriter(string attributeName,
                                     string propertyName,
                                     string argumentValue)
        {
            _attributeName = attributeName;
            _propertyName = propertyName;
            _argumentValue = argumentValue;
        }

        public override SyntaxNode? VisitAttribute(AttributeSyntax node)
        {
            if (node.Name.ToString() == _attributeName)
            {
                var alreadyHasArgument = node.ArgumentList?.Arguments
                    .Any(argument => argument.NameEquals?.Name.ToString() == _propertyName) ?? false;   

                if (alreadyHasArgument)
                {
                    return node;
                }

                var newArgument = SyntaxFactory
                    .AttributeArgument(SyntaxFactory
                    .NameEquals(_propertyName), null, SyntaxFactory
                    .LiteralExpression(SyntaxKind.StringLiteralExpression, SyntaxFactory
                    .Literal(_argumentValue)));

                AttributeArgumentListSyntax newArguments;

                if (node.ArgumentList != null)
                {
                    newArguments = node.ArgumentList.AddArguments(newArgument);
                }

                else
                {
                    newArguments = SyntaxFactory
                        .AttributeArgumentList(SyntaxFactory
                        .SeparatedList(new[] { newArgument}));
                }

                var newAttribute = node.WithArgumentList(newArguments);

                return newAttribute;
            }

            return base.VisitAttribute(node);
        }
    }
}
