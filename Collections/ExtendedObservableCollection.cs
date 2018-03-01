using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _180301_001_ExtendedObservableCollection.Collections
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class ExtendedObservableCollection<T> : ObservableCollection<T>
    {
        // 非公開静的フィールド
        private readonly string IndexerName = "Item[]";


        // コンストラクタ

        /// <summary>
        /// <see cref="ExtendedObservableCollection{T}"/> クラスの新しいインスタンスを初期化します。
        /// </summary>
        public ExtendedObservableCollection()
            : base()
        {
            // 実装なし
        }

        /// <summary>
        /// 指定したコレクションからコピーされた要素を格納する、<see cref="ExtendedObservableCollection{T}"/> クラスの新しいインスタンスを初期化します。
        /// </summary>
        /// <param name="collection"></param>
        public ExtendedObservableCollection(IEnumerable<T> collection)
            : base(collection)
        {
            // 実装なし
        }



        // 限定公開メソッド
        
        protected override void ClearItems()
        {
            var items = this.Items.ToArray();
            this.Items.Clear();

            this.OnPropertyChanged(new System.ComponentModel.PropertyChangedEventArgs(IndexerName));
            this.OnCollectionChanged(new ExtendedObservableCollectionChangedEventArgs(items));
        }


        // 公開メソッド

        /// <summary>
        /// 複数のアイテムを追加します。
        /// </summary>
        /// <param name="items"></param>
        public void AddRange(IEnumerable<T> items)
        {
            foreach (var item in items)
                this.Items.Add(item);

            this.OnPropertyChanged(new System.ComponentModel.PropertyChangedEventArgs(IndexerName));
            this.OnCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Add, items.ToArray()));
        }
    }

    /// <summary>
    /// 
    /// </summary>
    public class ExtendedObservableCollectionChangedEventArgs : NotifyCollectionChangedEventArgs
    {
        // 公開プロパティ

        /// <summary>
        /// Clear メソッドなどが実行された際に、直前までコレクションに存在していたアイテムの一覧を取得します。
        /// </summary>
        public IList PreviousItems
        {
            get;
            private set;
        }


        // コンストラクタ

        /// <summary>
        /// <see cref="NotifyCollectionChangedAction.Reset"/> 用の <see cref="ExtendedObservableCollectionChangedEventArgs"/> クラスの新しいインスタンスを初期化します。
        /// </summary>
        /// <param name="previousItems"></param>
        public ExtendedObservableCollectionChangedEventArgs(IEnumerable previousItems)
            : base(NotifyCollectionChangedAction.Reset)
        {
            this.PreviousItems = new List<object>(previousItems.Cast<Object>());
        }
    }
}
