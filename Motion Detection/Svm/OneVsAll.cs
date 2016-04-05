using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace libsvm
{
    class OneVsAll
    {
        public OneVsAll(System.Windows.Forms.TextBox info)
        {
            String[] str = new String[10];
            str[0] = "data_training/data-2410.train";
            str[1] = "model/svm.mdl";
            svm_train svmT = new svm_train(str);            
        }
    }
}
