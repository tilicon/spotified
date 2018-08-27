import { Component, Inject, Input, OnInit } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Artist } from "../models/artist";
import { Track } from "../models/track";
//import { MockTracks } from '../mock/mock-tracks';

@Component({
  selector: 'app-selected-recommendations',
  templateUrl: './selected-recommendations.component.html',
  styleUrls: ['./selected-recommendations.component.css']
})
export class SelectedRecommendationsComponent implements OnInit {
  @Input() selectedArtists: Artist[] = [];

  selectedRecommendations: Track[] = [];
  tracks: Track[] = [];

  httpClient: HttpClient;
  baseUrl: string;

  ngOnInit(): void {
    var query = "?artistids=";
    for (var i = 0; i < this.selectedArtists.length; i++) {
      query = query.concat(this.selectedArtists[i].id, ",");
    }

    this.httpClient.get<Track[]>(this.baseUrl + 'api/recommendations/tracks' + query).subscribe(result => {
        this.tracks = result;
      },
      error => console.error(error));
  }

  constructor(http: HttpClient, @Inject('BASE_URL') baseUrl: string) {
    this.httpClient = http;
    this.baseUrl = baseUrl;
  }

  //constructor() {
  //  this.tracks = MockTracks;
  //}
}
