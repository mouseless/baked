import{i,u as c,l,c as u,n as d,j as h,p as f,f as p,q as m,s as g,o as _,_ as w}from"./2lwfFFTD.js";const b=["src","alt","width","height"],R=i({__name:"ProseImg",props:{src:{default:""},alt:{default:""},width:{default:void 0},height:{default:void 0}},setup(a){const s=a,n=c(),r=l(()=>{if(s.src.startsWith("//"))return s.src;const e=f(p(m().app.baseURL)),t=o(n.path);return g(e,t,s.src)});function o(e){return e.endsWith("/")?e:e.replace(/\/[^/]*\/?$/,"")}return(e,t)=>(_(),u("img",{src:h(r),alt:e.alt,width:e.width,height:e.height,class:d([e.alt])},null,10,b))}}),I=w(R,[["__scopeId","data-v-6abb79d5"]]);export{I as default};
