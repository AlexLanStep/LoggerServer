
global using Prism.Events;
using System.Collections.Generic;

namespace DrawGraph.Models;

public class GraphLogInfo : PubSubEvent<List<LoggerOneDateSize>> { }
public class GraphLogInfoCar : PubSubEvent<List<LoggerOneDateSize>> { }


