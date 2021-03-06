﻿using System;
using Baseline;
using Oakton;

namespace Marten.CommandLine.Commands.Dump
{
    [Description("Dumps the entire DDL for the configured Marten database")]
    public class DumpCommand : MartenCommand<DumpInput>
    {
        public DumpCommand()
        {
            Usage("Writes the complete DDL for the entire Marten configuration to the named file")
                .Arguments(x => x.FileName);
        }

        protected override bool execute(IDocumentStore store, DumpInput input)
        {
            if (input.ByTypeFlag)
            {
                input.WriteLine("Writing DDL files to " + input.FileName);
                store.Schema.WriteDDLByType(input.FileName);
            }
            else
            {
                input.WriteLine("Writing DDL file to " + input.FileName);


                try
                {
                    new FileSystem().CleanDirectory(input.FileName);
                }
                catch (Exception)
                {
                    
                    input.WriteLine(ConsoleColor.Yellow, $"Unable to clean the directory at {input.FileName} before writing new files");
                }

                store.Schema.WriteDDL(input.FileName);
            }

            return true;
        }

    }
}