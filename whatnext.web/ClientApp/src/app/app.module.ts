import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';
import { RouterModule } from '@angular/router';

import { AppComponent } from './app.component';
import { NavMenuComponent } from './nav-menu/nav-menu.component';
import { HomeComponent } from './home/home.component';
import { FetchDataComponent } from './fetch-data/fetch-data.component';
import { RecommendationsComponent } from "./recommandations/recommendations.component";
import { SelectedArtistsComponent } from "./selected-artists/selected-artists.component";
import { SelectedCategoriesComponent } from "./selected-categories/selected-categories.component";
import { SelectedRecommendationsComponent } from "./selected-recommendations/selected-recommendations.component";

@NgModule({
  declarations: [
    AppComponent,
    NavMenuComponent,
    HomeComponent,
    FetchDataComponent,
    RecommendationsComponent,
    SelectedArtistsComponent,
    SelectedCategoriesComponent,
    SelectedRecommendationsComponent,
  ],
  imports: [
    BrowserModule.withServerTransition({ appId: 'ng-cli-universal' }),
    HttpClientModule,
    FormsModule,
    RouterModule.forRoot([
      { path: '', component: HomeComponent, pathMatch: 'full' },
      { path: 'fetch-data', component: FetchDataComponent },
      { path: 'recommendations', component: RecommendationsComponent },
      { path: 'selectedartists', component: SelectedArtistsComponent },
      { path: 'selectedcategories', component: SelectedCategoriesComponent },
      { path: "selectedrecommendations", component: SelectedRecommendationsComponent },
    ])
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
