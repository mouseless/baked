import{_ as w}from"./C7xy4FrH.js";import{j as B,A as h,B as E,C as N,o as s,c as r,a as f,n as g,k as m,F as k,y,i as S,b,w as x,d as C,t as I,_ as O}from"./DTm0dtNb.js";const P={key:1},V=B({__name:"Toc",props:{value:{}},setup(j){let _;const p=h(""),i=h(!1);function A(){i.value=!i.value}function v(){i.value=!1}return E(()=>{_=new IntersectionObserver(l,{root:document,rootMargin:"-75px"});const t={},c={};document.querySelectorAll(".toc-root > h2[id], .toc-root > h3[id]").forEach((e,u)=>{const d=e.getAttribute("id");c[d]=u}),document.querySelectorAll(".toc-root > *").forEach(e=>_.observe(e));function l(e){const u=[];for(const n of e){const o=a(n.target);o&&u.push({id:o,entry:n})}for(const{entry:n,id:o}of u)t[o]||(t[o]=0),n.isIntersecting?t[o]+=1:t[o]-=1;for(const n of Object.keys(t))t[n]<=0&&delete t[n];const d=Object.keys(t);d.sort((n,o)=>c[n]-c[o]),p.value=d[0]||""}function a(e){for(;c[e.getAttribute("id")||""]===void 0;){if(!e.previousElementSibling)return null;e=e.previousElementSibling}return e.getAttribute("id")}}),N(()=>{_.disconnect()}),(t,c)=>{const l=w;return s(),r("nav",null,[f("h4",null,[t.value.links.length>0?(s(),r("a",{key:0,onClick:A},"On This Page")):(s(),r("a",P," "))]),t.value.links.length>0?(s(),r("ul",{key:0,class:g({active:m(i)})},[(s(!0),r(k,null,y(t.value.links,a=>(s(),r("li",{key:a.id},[b(l,{to:`#${a.id}`,class:g({active:a.id===m(p)}),onClick:v},{default:x(()=>[C(I(a.text),1)]),_:2},1032,["to","class"]),f("ul",null,[(s(!0),r(k,null,y(a.children,e=>(s(),r("li",{key:e.id},[b(l,{to:`#${e.id}`,class:g({active:e.id===m(p)}),onClick:v},{default:x(()=>[C(I(e.text),1)]),_:2},1032,["to","class"])]))),128))])]))),128)),f("li",null,[f("a",{class:"return-to-top",href:"#",onClick:v},"Return to top")])],2)):S("",!0)])}}}),F=O(V,[["__scopeId","data-v-39cd5078"]]);export{F as default};
