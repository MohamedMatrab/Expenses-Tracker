import { Routes } from '@angular/router';

// ui
import { AppChipsComponent } from './chips/chips.component';
import { AppFormsComponent } from './forms/forms.component';
import { AppTablesComponent } from './categories/categories.component';

export const UiComponentsRoutes: Routes = [
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
        component: AppTablesComponent,
      },
    ],
  },
];
