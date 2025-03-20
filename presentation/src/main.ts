import './style.css'
import Reveal from "reveal.js";
import Markdown from "reveal.js/plugin/markdown/markdown";
import Highlight from "reveal.js/plugin/highlight/highlight";

import * as slides from 'slides';


function bla (){
    const code = {
        code: 3
    };
}
const deck = new Reveal({
    plugins: [
        Markdown,
        Highlight,
    ]
})
deck.initialize();




