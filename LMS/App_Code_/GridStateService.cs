//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Web;
//using LMS.Web.ViewModels.Shared;

//namespace LMS.Web
//{

//    public class GridStateService<TEntity, TSearchForm> where TSearchForm : SearchForm, new()
//    {
//        class GridState
//        {
//            public int CurrentPage;
//            public int PageSize;
//            public Sorter Sorter;
//            public TSearchForm SearchForm;
//        }

//        public void Save(string key, Grid<TEntity, TSearchForm> grid)
//        {
//            GridState state = new GridState
//                                {
//                                    CurrentPage = grid.Pager.CurrentPage,
//                                    PageSize = grid.Pager.PageSize,
//                                    Sorter = grid.Sorter,
//                                    SearchForm = grid.SearchForm
//                                };

//            HttpContext.Current.Session[key] = state;
//        }


//        public Grid<TEntity, TSearchForm> Load(string key)
//        {
//            if (HttpContext.Current.Session[key] == null)
//                return null;

//            GridState state = (GridState)HttpContext.Current.Session[key];

//            var grid = new Grid<TEntity, TSearchForm>();
//            grid.Pager.CurrentPage = state.CurrentPage;
//            grid.Pager.PageSize = state.PageSize;
//            grid.Sorter = state.Sorter;
//            grid.SearchForm = state.SearchForm;

//            return grid;
//        }

//        public bool Exists(string key)
//        {
//            Grid<TEntity, TSearchForm> grid = Load(key);
//            return (grid != null);
//        }
//    }
//}
