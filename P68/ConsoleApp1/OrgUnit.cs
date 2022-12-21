using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleApp1
{
    /// <summary>
    /// 自引用组织结构树
    /// </summary>
    public class OrgUnit
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public OrgUnit Parent { get; set; }
        public List<OrgUnit> Children { get; set; }= new List<OrgUnit>();
    }
}
