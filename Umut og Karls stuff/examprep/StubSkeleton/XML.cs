using System.Xml;

public class XMLWriter
{
    public static string ToXML(CommandStub command)
    {
        using (StringWriter sw = new StringWriter())
        {
            XmlWriterSettings settings = new XmlWriterSettings{
                OmitXmlDeclaration = true,
            };

            using (XmlWriter writer = XmlWriter.Create(sw, settings))
            {
                writer.WriteStartDocument();
                writer.WriteStartElement("Command");
                writer.WriteAttributeString("method", command.Method);
                writer.WriteStartElement("ArgumentList");
                foreach (string arg in command.Arguments)
                {
                    writer.WriteElementString("Argument", arg);
                }
                writer.WriteEndElement();
                writer.WriteEndElement();
                writer.WriteEndDocument();
            }

            return sw.ToString();
        }
    }

    public static string ToXML(CommandSkeleton[] commands)
    {
        using (StringWriter sw = new StringWriter())
        {
            XmlWriterSettings settings = new XmlWriterSettings();
            settings.OmitXmlDeclaration = true;

            using (XmlWriter writer = XmlWriter.Create(sw, settings))
            {
                writer.WriteStartElement("CommandList");
                foreach (CommandSkeleton command in commands)
                {
                    writer.WriteStartElement("Command");
                    writer.WriteAttributeString("method", command.Method);
                    writer.WriteElementString("Description", command.Description);
                    writer.WriteStartElement("ParameterList");
                    foreach (string param in command.Parameters)
                    {
                        writer.WriteElementString("Parameter", param);
                    }
                    writer.WriteEndElement();
                    writer.WriteEndElement();
                }
                writer.WriteEndElement();
            }

            return sw.ToString();
        }
    }
}

public class XMLReader
{
    public static List<CommandSkeleton> SkeletonsFromXML(string xml)
    {
        List<CommandSkeleton> commands = new();

        using (StringReader sr = new StringReader(xml))
        {
            XmlReaderSettings settings = new XmlReaderSettings{
                IgnoreComments = true,
                IgnoreWhitespace = true
            };

            using (XmlReader reader = XmlReader.Create(sr, settings))
            {
                reader.MoveToContent();
                if (reader.IsStartElement("CommandList"))
                {
                    reader.Read();
                }

                while (reader.IsStartElement("Command"))
                {
                    CommandSkeleton command = new CommandSkeleton();

                    command.Method = reader.GetAttribute("method") ?? "none";
                    reader.Read();

                    while (reader.IsStartElement())
                    {
                        if (reader.IsStartElement("Description"))
                        {
                            command.Description = reader.ReadElementContentAsString();
                        }
                        else if (reader.IsStartElement("ParameterList"))
                        {
                            reader.Read();

                            while (reader.IsStartElement("Parameter"))
                            {
                                command.Parameters.Add(reader.ReadElementContentAsString());
                            }
                        }
                        else
                        {
                            reader.Skip();
                        }
                    }

                    commands.Add(command);
                    
                    while (reader.NodeType == XmlNodeType.EndElement)
                    {
                        reader.Read();
                    }
                }
            }
        }

        return commands;
    }

    public static CommandStub StubFromXML(string xml)
    {
        CommandStub command = new CommandStub();

        using (StringReader sr = new StringReader(xml))
        {
            XmlReaderSettings settings = new XmlReaderSettings{
                IgnoreComments = true,
                IgnoreWhitespace = true
            };

            using (XmlReader reader = XmlReader.Create(sr, settings))
            {
                reader.MoveToContent();
                if (reader.IsStartElement("Command"))
                {
                    command.Method = reader.GetAttribute("method") ?? "none";
                    reader.Read();
                }

                if (reader.IsStartElement("ArgumentList"))
                {
                    reader.Read();
                    while (reader.IsStartElement("Argument"))
                    {
                        command.Arguments.Add(reader.ReadElementContentAsString());
                    }
                }

            }
        }

        return command;
    }
}