import { Component, Inject, Input, OnInit } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Artist } from "../models/artist";
import { Category } from "../models/category";
//import { MockArtists } from '../mock/mock-artists';

@Component({
  selector: 'app-selected-artists',
  templateUrl: './selected-artists.component.html',
  styleUrls: ['./selected-artists.component.css']
})
export class SelectedArtistsComponent implements OnInit{
  @Input() selectedCategories: Category[] = [];

  selectedArtists: Artist[] = [];
  setNumberOfArtists: number = 5;
  artists: Artist[] = [];

  httpClient: HttpClient;
  baseUrl: string;

  ngOnInit(): void {
    var query = "?genres=";
    for (var i = 0; i < this.selectedCategories.length; i++) {
      query = query.concat(this.selectedCategories[i].id, ",");
    }

    this.httpClient.get<Artist[]>(this.baseUrl + 'api/recommendations/artists' + query).subscribe(result => {
      this.artists = result;
    }, error => console.error(error));
  }

  constructor(http: HttpClient, @Inject('BASE_URL') baseUrl: string) {
    this.httpClient = http;
    this.baseUrl = baseUrl;
  }

  //constructor() {
  //  this.artists = MockArtists;
  //}

    getRemainingArtistCount() : number {
    if (!Array.isArray(this.selectedArtists))
      return this.setNumberOfArtists;

    return this.setNumberOfArtists - this.selectedArtists.length;
  }

  public select(artist: Artist) {
    artist.isSelected = !artist.isSelected;
    this.selectedArtists = [];
    for (var i = 0; i < this.artists.length; i++) {
      if (this.artists[i].isSelected) {
        this.selectedArtists.push(this.artists[i]);
      }
    }
  }
}
