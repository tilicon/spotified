import { Component, Inject, Input, OnInit } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Category } from "../models/category";
//import { MockCategories } from "../mock/mock-categories";

@Component({
  selector: 'app-selected-categories',
  templateUrl: './selected-categories.component.html',
  styleUrls: ['./selected-categories.component.css']
})
export class SelectedCategoriesComponent implements OnInit{
  @Input() selectedCategories: Category[] = [];
  setNumberOfCategories: number = 5;
  remainingCategoryCount: number = this.setNumberOfCategories - this.getSelectedCategoryCount();
  categories: Category[];
  selectedCateogries: Category[] = [];
  currentCount: number = 0;
  
  ngOnInit(): void {
  }

  constructor(http: HttpClient, @Inject('BASE_URL') baseUrl: string) {
    http.get<Category[]>(baseUrl + 'api/recommendations/categories').subscribe(result => {
      this.categories = result;
    }, error => console.error(error));
  }

  //constructor() {
  //  this.categories = MockCategories;
  //}

  getSelectedCategoryCount() : number {
    if (!Array.isArray(this.selectedCategories))
      return this.setNumberOfCategories;

    return this.setNumberOfCategories - this.selectedCategories.length;
  }

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
    this.selectedCategories = [];
    for (var i = 0; i < this.categories.length; i++) {
      if (this.categories[i].isSelected) {
        this.selectedCategories.push(this.categories[i]);
      }
    }
  }
}
