using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace KhelljyrBuilder
{
    public class SyntaxExplorer
    {
        public Builder Builder;

        private (Type, SyntaxExplorationResolver)[] Resolvers;

        public SyntaxExplorer(Builder builder)
        {
            Builder = builder;
            BuildResolvers();
        }

        private void BuildResolvers()
        {
            Resolvers = new (Type, SyntaxExplorationResolver)[]
            {
                (typeof(NamespaceDeclarationSyntax), new SyntaxExplorationResolver<NamespaceDeclarationSyntax>(ExploreNamespaceDeclaration)),
                (typeof(ClassDeclarationSyntax), new SyntaxExplorationResolver<ClassDeclarationSyntax>(ExploreClassDeclaration)),
                (typeof(MethodDeclarationSyntax), new SyntaxExplorationResolver<MethodDeclarationSyntax>(ExploreMethodDeclaration)),
            };
        }

        public void ExploreMember(MemberDeclarationSyntax member)
        {
            Type type = member.GetType();
            
            try
            {
                (Type, SyntaxExplorationResolver Resolver) found = Resolvers.First(i => i.Item1 == type);

                found.Resolver.Run(member);
            }
            catch (Exception e)
            {
                string str = $"Resolver not found {type.Name}";
                throw;
            }
        }

        public void ExploreMembers(IEnumerable<MemberDeclarationSyntax> members)
        {
            foreach (MemberDeclarationSyntax member in members)
            {
                ExploreMember(member);
            }
        }

        private void ExploreNamespaceDeclaration(NamespaceDeclarationSyntax member)
        {
            ExploreMembers(member.Members);
        }

        private void ExploreClassDeclaration(ClassDeclarationSyntax member)
        {
            ExploreMembers(member.Members);
        }

        private void ExploreMethodDeclaration(MethodDeclarationSyntax member)
        {
            foreach (StatementSyntax statementSyntax in member.Body.Statements)
            {
                
            }
        }
    }
}
