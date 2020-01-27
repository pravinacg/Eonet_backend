import { Component, OnInit, ViewChild } from '@angular/core';
import { MatTableDataSource, MatSort, MatPaginator } from '@angular/material';
import { ArticleService } from './article.service';
import { EonetService } from '../eonet.service';
import { eonetRoot} from './dataclass';
import { EventsEntity} from './dataclass';

@Component({
    selector: 'app-article',
    templateUrl: './article.component.html'
})
export class ArticleComponent implements OnInit {
    @ViewChild(MatSort) sort: MatSort;
    @ViewChild(MatPaginator) paginator: MatPaginator;    
  dataSource: MatTableDataSource<EventsEntity>;

    constructor(private eonet: EonetService) {}
    
    eventLst: eonetRoot[] = [];
    testEvent:any =[];
    errorMessage: string ="";
    eonetevents:EventsEntity[]=[];
    
    displayedColumns: string[] = ['id', 'title', 'description', 'link','category','source','geometries','closed'];
   
    
  
    ngOnInit() {
      this.getAllEvents();
      this.dataSource = new MatTableDataSource(this.eonetevents);
      this.dataSource.sort = this.sort;
      this.dataSource.paginator = this.paginator;
    }    
   
    getAllEvents()
    {
        this.eonet.getAllEvents()
        .subscribe(data => {
          this.testEvent = data;
          console.log(data);
         if(data != undefined && data !== null){
                console.log(data["title"]);
               this.eonetevents=data["events"];
               console.log(this.eonetevents);    
          }
    
        },
        error => this.errorMessage = <any>error);
    }
  

}