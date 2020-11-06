using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace DCETRuntime
{
    internal class OpcodeInfo
    {
        public string Name;
        public int Opcode;
        public string ParentInterface;
    }

    public static class Program
    {
        private static readonly char[] splitChars = { ' ', '\t' };
        private static readonly List<OpcodeInfo> messageOpcode = new List<OpcodeInfo>();
        private const string helpText = @"Usage: Proto2Opcode [-i:inFile] [-o:outFile] [-s:nameSpace] [-c:opcodeStart] [-e:className]
Arguments
-i              : Protocol input file
-o              : Protocol opcode file
-s              : opcode file namespace
-c              : opcode start number, [0,65535]
-e              : opcode file name


Options
-h              : Show the help message and exit";

        public static void Main(string[] args)
        {
            try
            {
                if (args != null)
                {
                    var inFile = string.Empty;
                    var outFile = string.Empty;
                    var nameSpace = string.Empty;
                    var className = string.Empty;
                    ushort opcodeStart = 0;

                    foreach (var arg in args)
                    {
                        if (string.IsNullOrWhiteSpace(arg))
                        {
                            continue;
                        }

                        if (arg.StartsWith("-i:"))
                        {
                            inFile = arg.Substring(3);
                        }
                        else if (arg.StartsWith("-o:"))
                        {
                            outFile = arg.Substring(3);
                        }
                        else if (arg.StartsWith("-s:"))
                        {
                            nameSpace = arg.Substring(3);
                        }
                        else if (arg.StartsWith("-c:"))
                        {
                            opcodeStart = ushort.Parse(arg.Substring(3));
                        }
                        else if (arg.StartsWith("-e:"))
                        {
                            className = arg.Substring(3);
                        }
                    }

                    Console.Error.WriteLine(outFile);

                    Proto2Opcode(nameSpace, inFile, outFile, opcodeStart, className);
                }
                else
                {
                    Console.WriteLine(helpText);
                }
            }
            catch(Exception e)
            {
                Console.Error.WriteLine(e.Message);
                Console.WriteLine(helpText);
            }
        }

        public static void Proto2Opcode(string nameSpace, string inputFile, string outputFile, ushort opcodeStart, string className)
        {
            messageOpcode.Clear();
            
            string s = File.ReadAllText(inputFile);

            foreach (string line in s.Split('\n'))
            {
                string newline = line.Trim();

                if (newline == "")
                {
                    continue;
                }

                if (newline.StartsWith("message"))
                {
                    var parentClass = string.Empty;
                    string msgName = newline.Split(splitChars, StringSplitOptions.RemoveEmptyEntries)[1];
                    string[] ss = newline.Split(new[] { "//" }, StringSplitOptions.RemoveEmptyEntries);

                    if (ss.Length == 2)
                    {
                        parentClass = ss[1].Trim();
                    }

                    if (!string.IsNullOrWhiteSpace(parentClass))
                    {
                        messageOpcode.Add(new OpcodeInfo() { Name = msgName, Opcode = ++opcodeStart, ParentInterface = parentClass });
                    }
                }
            }

            GenerateOpcode(nameSpace, outputFile, className);
        }

        private static void GenerateOpcode(string ns, string outputPath, string className)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine($"namespace {ns}");
            sb.AppendLine("{");

            foreach (OpcodeInfo info in messageOpcode)
            {
                if (!string.IsNullOrWhiteSpace(info.ParentInterface))
                {
                    sb.Append($"\t[Message({className}.{info.Name})]\n");
                    sb.AppendLine($"\tpublic partial class {info.Name} : {info.ParentInterface}");
                    sb.AppendLine("\t{");
                    sb.AppendLine("\t}");
                }
            }

            sb.AppendLine($"\tpublic static partial class {className}");
            sb.AppendLine("\t{");

            foreach (OpcodeInfo info in messageOpcode)
            {
                sb.AppendLine($"\t\t public const ushort {info.Name} = {info.Opcode};");
            }

            sb.AppendLine("\t}");
            sb.AppendLine("}");

            File.WriteAllText(outputPath, sb.ToString());
        }
    }
}
