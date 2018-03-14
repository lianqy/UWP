using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Todos.Models;
using Todos.ViewModels;
using System.Xml.Linq;
using Windows.Data.Xml.Dom;
using Windows.UI.Notifications;


namespace Todos
{
    class TileService
    {
        public static Windows.Data.Xml.Dom.XmlDocument CreateTiles(TodoItem todoitem)
        {
            string dateText = "" + todoitem.date.Year + '-' + todoitem.date.Month + '-' + todoitem.date.Day;
            XDocument xDoc = new XDocument(
                new XElement("tile", new XAttribute("version", 3),
                    new XElement("visual",
                        // Small Tile
                        new XElement("binding", new XAttribute("displayName", todoitem.title), new XAttribute("template", "TileSmall"),
                   //     new XElement("image", new XAttribute("src", "Assets\background.jpg"), new XAttribute("placement", "background")),
                            new XElement("group",
                                new XElement("subgroup",
                                    new XElement("text", todoitem.title, new XAttribute("hint-style", "caption"), new XAttribute("hint-wrap", "true"))
                                //  new XElement("text", dateText, new XAttribute("hint-style", "caption")),
                                // new XElement("text", todoitem.description, new XAttribute("hint-style", "captionsubtle"), new XAttribute("hint-wrap", true), new XAttribute("hint-maxLines", 3))
                                )
                            )
                        ),

                        // Medium Tile
                        new XElement("binding", new XAttribute("displayName", todoitem.title), new XAttribute("template", "TileMedium"),
                   //     new XElement("image", new XAttribute("src", "Assets\background.jpg"), new XAttribute("placement", "background")),
                            new XElement("group",
                                new XElement("subgroup",
                                    new XElement("text", todoitem.title, new XAttribute("hint-style", "caption")),
                                    new XElement("text", dateText, new XAttribute("hint-style", "caption")),
                                    new XElement("text", todoitem.description, new XAttribute("hint-style", "captionsubtle"), new XAttribute("hint-wrap", true), new XAttribute("hint-maxLines", 3))
                                )
                            )
                        ),

                        // Wide Tile
                        new XElement("binding", new XAttribute("template", "TileWide"),
                      //  new XElement("image", new XAttribute("src", "Assets\background.jpg"), new XAttribute("placement", "background")),
                           new XElement("group",
                               new XElement("subgroup",
                                   new XElement("text", todoitem.title, new XAttribute("hint-style", "caption")),
                                    new XElement("text", dateText, new XAttribute("hint-style", "caption")),
                                    new XElement("text", todoitem.description, new XAttribute("hint-style", "captionsubtle"), new XAttribute("hint-wrap", true), new XAttribute("hint-maxLines", 3))
                                   )
                           )
                       ),

                       //Large Tile
                       new XElement("binding", new XAttribute("template", "TileLarge"),
                    //   new XElement("image", new XAttribute("src", "Assets\background.jpg"), new XAttribute("placement", "background")),
                           new XElement("group",
                               new XElement("subgroup",
                                  new XElement("text", todoitem.title, new XAttribute("hint-style", "caption")),
                                    new XElement("text", dateText, new XAttribute("hint-style", "caption")),
                                    new XElement("text", todoitem.description, new XAttribute("hint-style", "captionsubtle"), new XAttribute("hint-wrap", true), new XAttribute("hint-maxLines", 3))
                                   )
                           )
                       )

                    )
                )
            );

            Windows.Data.Xml.Dom.XmlDocument xmlDoc = new Windows.Data.Xml.Dom.XmlDocument();
            xmlDoc.LoadXml(xDoc.ToString());
           // xmlDoc.LoadXml(xDoc);
            return xmlDoc;
        }
    }
}
