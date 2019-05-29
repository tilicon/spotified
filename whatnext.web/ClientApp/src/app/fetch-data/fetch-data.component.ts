import { Component, Inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Category } from "../models/category";
//import { MockCategories } from '../mock/mock-categories';

@Component({
  selector: 'app-fetch-data',
  templateUrl: './fetch-data.component.html'
})

export class FetchDataComponent {
  public categories: Category[];
  public currentCount = 0;
  public selectedCategory: Category;
  public selectedCategories: Category[];

  constructor(http: HttpClient, @Inject('BASE_URL') baseUrl: string) {
    //http.get<Category[]>(baseUrl + 'api/recommendations/categories').subscribe(result => {
    //  this.categories = result;
    //}, error => console.error(error));
  }
  //constructor() {
  //  this.categories = MockCategories;
  //}

  public adjustCounter(isIncreasing) {
    if (isIncreasing) {
      this.currentCount++;
    } else {
      this.currentCount--;
    }
  }

  public select(category: Category) {
    category.isSelected = !category.isSelected;
    this.adjustCounter(category.isSelected);
    this.selectedCategory = category;
    this.selectedCategories = [];
    for (var i = 0; i < this.categories.length; i++) {      
      if (this.categories[i].isSelected) {
        this.selectedCategories.push(this.categories[i]);
      }
    }
  }
}
