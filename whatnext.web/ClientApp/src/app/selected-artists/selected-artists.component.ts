import { Component, Inject, Input, OnInit } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Artist } from "../models/artist";
import { MockArtists } from '../mock/mock-artists';

@Component({
  selector: 'app-selected-artists',
  templateUrl: './selected-artists.component.html',
  styleUrls: ['./selected-artists.component.css']
})
export class SelectedArtistsComponent implements OnInit{
  @Input() selectedArtists: Artist[] = [];
  setNumberOfArtists: number = 5;
  artists: Artist[] = [];

  constructor() {
    this.artists = MockArtists;
  }

  ngOnInit(): void {
  }

  //constructor(http: HttpClient, @Inject('BASE_URL') baseUrl: string) {
  //  http.get<Category[]>(baseUrl + 'api/recommendations/categories').subscribe(result => {
  //    this.categories = result;
  //  }, error => console.error(error));
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
