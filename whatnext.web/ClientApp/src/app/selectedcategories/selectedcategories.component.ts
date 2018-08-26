import { Component, Inject, Input } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Category } from "../models/category";
import { Artist } from "../models/artist";
import { MockArtists } from '../mock/mock-artists';

@Component({
  selector: 'app-selectedcategories',
  templateUrl: './selectedcategories.component.html',
  styleUrls: ['./selectedcategories.component.css']
})
export class SelectedCategoriesComponent implements OnInit{
  @Input() selectedCategories: Category[] = [];
  setNumberOfCategories: number = 5;
  remainingCategoryCount: number = this.setNumberOfCategories - this.getSelectedCategoryCount();
  artists: Artist[] = [];

  constructor() {
    this.artists = MockArtists;
  }

  //constructor(http: HttpClient, @Inject('BASE_URL') baseUrl: string) {
  //  http.get<Category[]>(baseUrl + 'api/recommendations/categories').subscribe(result => {
  //    this.categories = result;
  //  }, error => console.error(error));
  //}

  getSelectedCategoryCount() : number {
    if (!Array.isArray(this.selectedCategories))
      return this.setNumberOfCategories;

    return this.setNumberOfCategories - this.selectedCategories.length;
  }
}
