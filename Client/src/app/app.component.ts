import { Component } from '@angular/core';

import { Store } from "@ngrx/store";
import { Observable } from 'rxjs';

import * as $ from 'jquery';

import { Post } from "./_models/post.model";
import * as PostActions from "./_actions/post.action";



interface AppState {
  message: string,
  post: Post,
}

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent {
  title = 'Client';

  message$: Observable<string>;

  post: Observable<Post>;
  text: string;

  constructor(private store: Store<AppState>) {
    this.message$ = this.store.select('message');
    this.post = this.store.select('post');
  }

  editText() {
    this.store.dispatch(new PostActions.EditText(this.text));
  }

  resetText() {
    this.store.dispatch(new PostActions.Reset());
  }

  upvote() {
    this.store.dispatch(new PostActions.Upvote());
  }

  downvote() {
    this.store.dispatch(new PostActions.Downvote());
  }

  spanishMessage() {
    this.store.dispatch({type: 'SPANISH'});
  }

  frenchMessage() {
    this.store.dispatch({type: 'FRENCH'});
  }
}
