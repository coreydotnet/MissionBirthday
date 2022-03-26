import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { DataAdminPageComponent } from './pages/data-admin-page/data-admin-page.component';
import { HomePageComponent } from './pages/home/home-page.component';
import { ResourceFormPageComponent } from './pages/resource-form/resource-form-page.component';
import { SearchResultsPageComponent } from './pages/search-results/search-results-page.component';

const routes: Routes = [
  {
    path: '',
    pathMatch: 'full',
    component: HomePageComponent
  },
  {
    path: 'search/:zipCode',
    component: SearchResultsPageComponent
  },
  {
    path: 'add-resource',
    component: ResourceFormPageComponent
  },
  {
    path: 'admin',
    children: [
      {
        path: 'data',
        component: DataAdminPageComponent
      }
    ]
  }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
