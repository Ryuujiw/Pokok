import { Component, OnInit, Inject } from '@angular/core';
import * as L from 'leaflet';
import { HttpClient } from '@angular/common/http';
import 'leaflet.heat/dist/leaflet-heat.js'
import { getBaseUrl } from '../../main';

@Component({
  selector: 'app-map',
  templateUrl: './map.component.html',
  styleUrls: ['./map.component.css']
})

export class MapComponent implements OnInit {

  private dataPoints: HeatmapDataPoints[];
  private baseUrl: string;

  constructor(private http: HttpClient) {
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

    this.http.get<HeatmapDataPoints[]>(getBaseUrl() + 'heatmap').subscribe(result => {
      this.dataPoints = result;

      let addressPoints: [number, number, number][] = this.dataPoints.map(function (item) {
        //return new L.LatLng(item.latitude, item.longitude);
        return [item.latitude, item.longitude, item.weight];
      });

    (L as any).heatLayer(addressPoints, { radius: 25 }).addTo(map);

    }, error => console.error(error));
  }

  ngOnInit(): void {
  }
}

interface HeatmapDataPoints {
  latitude: number;
  longitude: number;
  weight: number;
}
