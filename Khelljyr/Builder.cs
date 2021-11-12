using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace KhelljyrBuilder
{
    public class Builder
    {
        private static readonly IEnumerable<string> DefaultNamespaces =
            new[]
            {
                "System",
                "System.IO",
                "System.Net",
                "System.Linq",
                "System.Text",
                "System.Text.RegularExpressions",
                "System.Collections.Generic"
            };

        private static List<MetadataReference> DefaultReferences =
            new List<MetadataReference>
            {
                MetadataReference.CreateFromFile(typeof(object).Assembly.Location),
                MetadataReference.CreateFromFile(typeof(Object).Assembly.Location),
                MetadataReference.CreateFromFile(typeof(EnumerableQuery).Assembly.Location),
                MetadataReference.CreateFromFile(typeof(System.Text.RegularExpressions.Capture).Assembly.Location),
                MetadataReference.CreateFromFile(typeof(Console).Assembly.Location),
            };

        private static readonly CSharpCompilationOptions DefaultCompilationOptions =
            new CSharpCompilationOptions(OutputKind.DynamicallyLinkedLibrary)
                .WithOverflowChecks(true).WithOptimizationLevel(OptimizationLevel.Release)
                .WithUsings(DefaultNamespaces);

        public void Compile(string str)
        {
            SyntaxTree tree = CSharpSyntaxTree.ParseText(str);
            CompilationUnitSyntax root = tree.GetCompilationUnitRoot();

            Assembly.GetEntryAssembly().GetReferencedAssemblies().ToList()
                .ForEach(e => DefaultReferences.Add(MetadataReference.CreateFromFile(Assembly.Load(e).Location)));

            var a = CSharpCompilation.Create("truc.dll", new SyntaxTree[]
            {
                root.SyntaxTree
            }, DefaultReferences, DefaultCompilationOptions);

            var b = a.Emit("temp.dll");
            SyntaxExplorer explorer = new SyntaxExplorer(this);
            
            explorer.ExploreMembers(root.Members);
        }
    }
}
