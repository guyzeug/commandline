﻿// Copyright 2005-2013 Giacomo Stelluti Scala & Contributors. All rights reserved. See doc/License.md in the project root for license information.

using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using CommandLine.Core;
using CommandLine.Infrastructure;
using CommandLine.Tests.Fakes;
using Xunit;

namespace CommandLine.Tests.Unit.Core
{
    public class TokenizerTests
    {
        [Fact]
        public void Explode_scalar_with_separator_returns_sequence()
        {
            // Fixture setup
            var expectedTokens = new[] { Token.Name("string-seq"),
                Token.Value("aaa"), Token.Value("bb"),  Token.Value("cccc")};
            var specs = new[] { new OptionSpecification(string.Empty, "string-seq",
                false, string.Empty, -1, -1, ",", null, typeof(IEnumerable<string>), string.Empty, string.Empty, new List<string>())};

            // Exercize system
            var result =
                Tokenizer.ExplodeOptionList(
                    StatePair.Create(
                        Enumerable.Empty<Token>().Concat(new[] { Token.Name("string-seq"), Token.Value("aaa,bb,cccc") }),
                        Enumerable.Empty<Error>()),
                        optionName => "string-seq".EqualsOrdinal(optionName) ? Maybe.Just(",") : Maybe.Nothing<string>());

            // Verify outcome
            Assert.True(expectedTokens.SequenceEqual(result.Value));

            // Teardown
        }
    }
   
}