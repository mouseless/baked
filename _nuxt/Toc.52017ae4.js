import{_ as w}from"./nuxt-link.d3ee2cf3.js";import{i as B,y,z as E,A as N,o as s,c as a,a as f,n as g,j as m,F as h,B as k,k as S,b,w as x,d as I,t as C,_ as O}from"./entry.d8e9bf6d.js";const P={key:1},T=B({__name:"Toc",props:{value:{}},setup(V){let _;const p=y(""),i=y(!1);function A(){i.value=!i.value}function v(){i.value=!1}return E(()=>{_=new IntersectionObserver(l,{root:document,rootMargin:"-75px"});const t={},c={};document.querySelectorAll(".toc-root > h2[id], .toc-root > h3[id]").forEach((e,u)=>{const d=e.getAttribute("id");c[d]=u}),document.querySelectorAll(".toc-root > *").forEach(e=>_.observe(e));function l(e){const u=[];for(const n of e){const o=r(n.target);o&&u.push({id:o,entry:n})}for(const{entry:n,id:o}of u)t[o]||(t[o]=0),n.isIntersecting?t[o]+=1:t[o]-=1;for(const n of Object.keys(t))t[n]<=0&&delete t[n];const d=Object.keys(t);d.sort((n,o)=>c[n]-c[o]),p.value=d[0]||""}function r(e){for(;c[e.getAttribute("id")||""]===void 0;){if(!e.previousElementSibling)return null;e=e.previousElementSibling}return e.getAttribute("id")}}),N(()=>{_.disconnect()}),(t,c)=>{const l=w;return s(),a("nav",null,[f("h4",null,[t.value.links.length>0?(s(),a("a",{key:0,onClick:A},"On This Page")):(s(),a("a",P," "))]),t.value.links.length>0?(s(),a("ul",{key:0,class:g({active:m(i)})},[(s(!0),a(h,null,k(t.value.links,r=>(s(),a("li",{key:r.id},[b(l,{to:`#${r.id}`,class:g({active:r.id===m(p)}),onClick:v},{default:x(()=>[I(C(r.text),1)]),_:2},1032,["to","class"]),f("ul",null,[(s(!0),a(h,null,k(r.children,e=>(s(),a("li",{key:e.id},[b(l,{to:`#${e.id}`,class:g({active:e.id===m(p)}),onClick:v},{default:x(()=>[I(C(e.text),1)]),_:2},1032,["to","class"])]))),128))])]))),128)),f("li",null,[f("a",{class:"return-to-top",href:"#",onClick:v},"Return to top")])],2)):S("",!0)])}}});const z=O(T,[["__scopeId","data-v-4f8affad"]]);export{z as default};
