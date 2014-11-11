using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace iReader.Controls
{
	class MyStackPanel : VirtualizingStackPanel
	{
		public override void PageDown()
		{
			
			//base.PageDown();
		}

        public override void PageUp()
        {
            foreach (var child in this.Children)
            {
                
            }
            //base.PageUp();
        }

        //protected override System.Windows.Size MeasureOverride(System.Windows.Size constraint)
        //{
        //    //return base.MeasureOverride(constraint);
        //    return new System.Windows.Size(460, 800);
        //}
	}
}
