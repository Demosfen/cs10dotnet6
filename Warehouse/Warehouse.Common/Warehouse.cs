using System;
using System.Collections.Generic;
using static System.Console;

namespace Warehouse.Common;

public sealed class Warehouse : Object
{
    public List<Pallet> Pallets = new ();
}
