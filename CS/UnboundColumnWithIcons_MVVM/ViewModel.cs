using DevExpress.Mvvm;
using DevExpress.Mvvm.DataAnnotations;
using DevExpress.Mvvm.Xpf;
using System;
using System.Collections.ObjectModel;
using System.Windows.Media.Imaging;

namespace UnboundColumnWithIcons_MVVM {
    public class MyObject {
        public string Action { get; set; }
        public MyObject(string action) {
            Action = action;
        }
    }

    public class ViewModel : ViewModelBase {
        public ObservableCollection<MyObject> Items { get; set; }

        public ViewModel() {
            Items = new ObservableCollection<MyObject> {
                new MyObject("cut"),
                new MyObject("copy"),
                new MyObject("paste"),
                new MyObject("delete")
            };
        }

        [Command]
        public void UnboundColumnData(UnboundColumnRowArgs args) {
            if(args.FieldName == "IconUnbound" && args.IsGetData) {
                var item = (MyObject)args.Item;
                var resourceName = GetResourceName(item.Action);
                args.Value = GetImage(resourceName);
            }
        }

        BitmapFrame GetImage(string resourceName) {
            var uri = new Uri("pack://application:,,,/Icons/" + resourceName, UriKind.Absolute);
            return BitmapFrame.Create(uri);
        }
        string GetResourceName(string action) {
            switch(action) {
                case "copy":
                    return "copy32x32.png";
                case "cut":
                    return "cut32x32.png";
                case "delete":
                    return "delete32x32.png";
                case "paste":
                    return "paste32x32.png";
                default:
                    return string.Empty;
            }
        }
    }
}
