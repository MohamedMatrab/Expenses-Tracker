import { Routes } from '@angular/router';

// ui
import { AppChipsComponent } from './chips/chips.component';
import { AppFormsComponent } from './forms/forms.component';
import { AppCategoriesComponent } from './categories/categories.component';
import {CategoriesListResolver} from "../../resolvers/category/categories-list.resolver";

export const OtherRoutes: Routes = [
  {
    path: '',
    children: [
      {
        path: 'chips',
        component: AppChipsComponent,
      },
      {
        path: 'forms',
        component: AppFormsComponent,
      },
      {
        path: 'categories',
        component: AppCategoriesComponent,
        resolve:{
          categories:CategoriesListResolver
        }
      },
    ],
  },
];
