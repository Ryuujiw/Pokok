import { Component, OnInit, Inject } from '@angular/core';
import * as L from 'leaflet';
import { HttpClient } from '@angular/common/http';
import 'leaflet.heat/dist/leaflet-heat.js'
import { FormControl, FormGroup, FormBuilder } from '@angular/forms';

@Component({
  selector: 'app-map',
  templateUrl: './map.component.html',
  styleUrls: ['./map.component.css'],
  template: `
    New Tree: <input type="text" [formControl]="addingNewTree">
  `
})

export class MapComponent implements OnInit {

  private dataPoints: HeatmapDataPoints[];
  private baseUrl: string;

  form: FormGroup;
  addingNewTree = new FormControl('');

  constructor(@Inject('BASE_URL') baseUrl: string, private http: HttpClient, private fb: FormBuilder) {
    this.baseUrl = baseUrl;
  }

  options = {
    layers: [
      L.tileLayer('https://{s}.tile.openstreetmap.org/{z}/{x}/{y}.png', {
        attribution: '&copy; OpenStreetMap contributors'
      })
    ],
    gradient: { 0.4: 'grey', 0.65: 'brown', 1: 'lime' },
    zoom: 15,
    center: L.latLng([3.0726965, 101.5899273])
  };

  onMapReady(map: L.Map) {

    this.http.get<HeatmapDataPoints[]>(this.baseUrl + 'api/tree').subscribe(result => {
      this.dataPoints = result;

      let addressPoints: [number, number, number][] = this.dataPoints.map(function (item) {
        //return new L.LatLng(item.latitude, item.longitude);
        return [item.latitude, item.longitude, item.weight];
      });

    (L as any).heatLayer(addressPoints, { radius: 25 }).addTo(map);

    }, error => console.error(error));
  }

  ngOnInit(): void {
    this.form = this.fb.group({
      latitude: '',
      longitude: '',
      species: ''
    })

    this.form.valueChanges.subscribe(console.log());
  }
}

interface HeatmapDataPoints {
  latitude: number;
  longitude: number;
  weight: number;
}
