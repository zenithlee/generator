﻿// Generated by Xamasoft JSON Class Generator
// http://www.xamasoft.com/json-class-generator

using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Generator.ClarifaiJsonTypes;

namespace Generator.ClarifaiJsonTypes
{

    internal class Tag
    {

        [JsonProperty("timestamp")]
        public double Timestamp { get; set; }

        [JsonProperty("model")]
        public string Model { get; set; }

        [JsonProperty("config")]
        public string Config { get; set; }
    }

    internal class Meta
    {

        [JsonProperty("tag")]
        public Tag Tag { get; set; }
    }

    internal class Tag2
    {

        [JsonProperty("concept_ids")]
        public string[] ConceptIds { get; set; }

        [JsonProperty("classes")]
        public string[] Classes { get; set; }

        [JsonProperty("probs")]
        public double[] Probs { get; set; }
    }

    internal class Result2
    {

        [JsonProperty("tag")]
        public Tag2 Tag { get; set; }
    }

    internal class Result
    {

        [JsonProperty("docid")]
        public double Docid { get; set; }

        [JsonProperty("url")]
        public string Url { get; set; }

        [JsonProperty("status_code")]
        public string StatusCode { get; set; }

        [JsonProperty("status_msg")]
        public string StatusMsg { get; set; }

        [JsonProperty("local_id")]
        public string LocalId { get; set; }

        [JsonProperty("result")]
        public Result2 Resulted { get; set; }

        [JsonProperty("docid_str")]
        public string DocidStr { get; set; }
    }

}

namespace Generator
{

    internal class Clarifai
    {

        [JsonProperty("status_code")]
        public string StatusCode { get; set; }

        [JsonProperty("status_msg")]
        public string StatusMsg { get; set; }

        [JsonProperty("meta")]
        public Meta Meta { get; set; }

        [JsonProperty("results")]
        public Result[] Results { get; set; }
    }

}
