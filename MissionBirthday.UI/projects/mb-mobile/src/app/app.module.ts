import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { IonicModule } from '@ionic/angular';
import { HoundBotModule } from 'hound-bot';
import { HttpClientModule } from '@angular/common/http';
import { HomePageComponent } from './pages/home/home-page.component';
import { SearchResultsPageComponent } from './pages/search-results/search-results-page.component';
import { ResourceFormPageComponent } from './pages/resource-form/resource-form-page.component';
import { ReactiveFormsModule } from '@angular/forms';
import { ItemListPipe } from './pages/search-results/pipes/item-list.pipe';
import { DataAdminPageComponent } from './pages/data-admin-page/data-admin-page.component';
import { SampleImagePickerComponent } from './pages/resource-form/sample-image-picker/sample-image-picker.component';

@NgModule({
  declarations: [
    AppComponent,
    HomePageComponent,
    SearchResultsPageComponent,
    ResourceFormPageComponent,
    ItemListPipe,
    DataAdminPageComponent,
    SampleImagePickerComponent
  ],
  imports: [
    HttpClientModule,
    BrowserModule,
    AppRoutingModule,
    HoundBotModule,
    ReactiveFormsModule,
    IonicModule.forRoot()
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
