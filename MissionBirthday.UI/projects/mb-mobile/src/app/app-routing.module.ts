import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
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
  }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
